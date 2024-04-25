namespace Aira.Infrastructure.Contexts;

public class ApplicationDbContext(
	DbContextOptions<ApplicationDbContext> options,
	IUser user,
	IDomainEventDispatcher dispatcher)
	: IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
	private readonly IUser _user = user ?? throw new ArgumentNullException(nameof(user));
	private readonly IDomainEventDispatcher _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

	#region DbSets



	#endregion DbSets

	#region Overrides

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (Debugger.IsAttached)
		{
			optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		base.OnModelCreating(modelBuilder);
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		UpdateModelStatuses();

		var result = await base.SaveChangesAsync(cancellationToken);

		// ignore events if no dispatcher provided
		if (_dispatcher == null) return result;

		// dispatch events only if save was successful
		var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
			.Select(e => e.Entity)
			.Where(e => e.DomainEvents.Any())
			.ToArray();

		await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

		return result;
	}

	#endregion Overrides

	#region Private methods


	private void UpdateModelStatuses()
	{
		foreach (var entry in ChangeTracker.Entries())
		{
			switch (entry.State)
			{
				case EntityState.Added:
					if (entry.Entity.GetType().BaseType == typeof(BaseEntity))
					{
						entry.CurrentValues["CreatedBy"] = _user.Id;
						entry.CurrentValues["CreatedDateTime"] = DateTime.UtcNow;
					}
					break;
				case EntityState.Modified:
					if (entry.Entity.GetType().BaseType == typeof(BaseEntity))
					{
						entry.CurrentValues["LastModifiedBy"] = _user.Id;
						entry.CurrentValues["LastModifiedDateTime"] = DateTime.UtcNow;
					}
					break;
				case EntityState.Detached:
					break;
				case EntityState.Unchanged:
					break;
				case EntityState.Deleted:
					if (entry.Entity.GetType().BaseType == typeof(BaseEntity))
					{
						entry.State = EntityState.Modified;
						entry.CurrentValues["LastModifiedBy"] = _user.Id;
						entry.CurrentValues["LastModifiedDateTime"] = DateTime.UtcNow;
						//TODO: Note for Denis - Soft deletes (NEVER DELETE ANYTHING!)
						entry.CurrentValues["IsDeleted"] = true;
					}
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}

	#endregion Private methods


}
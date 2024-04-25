namespace Aira.Application.Interfaces;

public interface IApplicationDbContext
{
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	DatabaseFacade Database { get; }

	DbSet<TEntity> Set<TEntity>() where TEntity : class;

	EntityEntry Entry(object entity);

	#region DbSets


	#endregion DbSets
}
namespace Aira.Infrastructure;

public static class DependencyInjection
{
	public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddDbContext(configuration);
		services.AddCaching();
		services.AddDomainEvents();
	}

	#region DbContexts

	private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");

		services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlServer(connectionString,
				builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
			options.ConfigureWarnings(builder =>
			{
				builder.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning);
			});
		});

		services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>();

		services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
	}

	#endregion DbContexts

	#region Caching

	private static void AddCaching(this IServiceCollection services)
	{
		services.AddDistributedMemoryCache();
	}

	#endregion Caching

	#region Domain Events

	private static void AddDomainEvents(this IServiceCollection services)
	{
		services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
	}

	#endregion Domain Events
}
namespace Aira.Mvc;

public static class DependencyInjection
{
	public static IServiceCollection AddWebServices(this IServiceCollection services)
	{
		services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();
		return services;
	}
}
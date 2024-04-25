namespace Aira.Mvc;

public static class DependencyInjection
{
	public static IServiceCollection AddWebServices(this IServiceCollection services)
	{
		services.AddScoped<IUser, CurrentUser>();
		services.AddHttpContextAccessor();

		return services;
	}
}
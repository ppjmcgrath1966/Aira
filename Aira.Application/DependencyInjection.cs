namespace Aira.Application;

public static class DependencyInjection
{
	public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddMediator();
		services.AddHangfire(configuration);
		services.AddMultiLingualSupport();
	}

	#region MediatR

	private static void AddMediator(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
	}

	#endregion MediatR

	#region Hangfire

	private static void AddHangfire(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
		services.AddHangfireServer();
	}


	#endregion Hangfire

	#region Multi Lingual Support

	private static void AddMultiLingualSupport(this IServiceCollection services)
	{
		#region Registering ResourcesPath

		services.AddLocalization(options => options.ResourcesPath = "");

		#endregion Registering ResourcesPath

		services.AddMvc()
			.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
			.AddDataAnnotationsLocalization(options =>
			{
				// ReSharper disable once AssignNullToNotNullAttribute
				var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName!);
				options.DataAnnotationLocalizerProvider = (_, factory) => factory.Create("SharedResource", assemblyName.Name!);
			});
		services.Configure<RequestLocalizationOptions>(options =>
		{
			var cultures = new List<CultureInfo> {
				new("en"),
				new("es"),
				new("pt"),
				new("pl")
			};
			options.DefaultRequestCulture = new RequestCulture("en");
			options.SupportedCultures = cultures;
			options.SupportedUICultures = cultures;
			options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
		});
		services.AddSingleton<SharedResourceService>();
	}


	#endregion Multi Lingual Support
}
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();


builder.Services.AddHttpClient(nameof(HttpFactoryClients.AiraApi), httpClient =>
{
	httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseApiAddress")!);
	httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
	httpClient.DefaultRequestHeaders.Add("XApiKey", builder.Configuration.GetValue<string>("XApiKey"));
});

builder.Services.AddNotyf(o =>
{
    o.DurationInSeconds = 10;
    o.IsDismissable = true;
    o.HasRippleEffect = true;
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

var supportedCultures = new[] { "en", "es", "pt", "pl" };
var localizationOptions = new RequestLocalizationOptions()
	.SetDefaultCulture(supportedCultures[0])
	.AddSupportedCultures(supportedCultures)
	.AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseNotyf();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient(nameof(HttpFactoryClients.AiraApi), httpClient =>
{
	httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseApiAddress")!);
	httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
	httpClient.DefaultRequestHeaders.Add("XApiKey", builder.Configuration.GetValue<string>("XApiKey"));
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

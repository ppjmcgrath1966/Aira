namespace Aira.Api.Middleware;

public class ApiKeyMiddleware(RequestDelegate next)
{
	private const string Apikey = "XApiKey";

	public async Task InvokeAsync(HttpContext context)
	{
		if (!context.Request.Headers.TryGetValue(Apikey, out var extractedApiKey))
		{
			context.Response.StatusCode = 401;
			await context.Response.WriteAsync("Api Key was not provided ");
			return;
		}

		var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();

		var apiKey = appSettings.GetValue<string>(Apikey);

		if (!apiKey.Equals(extractedApiKey))
		{
			context.Response.StatusCode = 401;
			await context.Response.WriteAsync("Unauthorized client");
			return;
		}

		await next(context);
	}
}
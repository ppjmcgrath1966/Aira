namespace Aira.Resources.Services;

public class SharedResourceService
{
	private readonly IStringLocalizer _localizer;

	public SharedResourceService(IStringLocalizerFactory factory)
	{
		var type = typeof(SharedResource);
		// ReSharper disable once AssignNullToNotNullAttribute
		var assemblyFullName = type.GetTypeInfo().Assembly.FullName;
		if (assemblyFullName == null) return;
		var assemblyName = new AssemblyName(assemblyFullName);
		_localizer = factory.Create("SharedResource", assemblyName.Name ?? string.Empty);
	}

	public LocalizedString this[string key] => _localizer[key];

	public LocalizedString GetLocalizedString(string key)
	{
		return _localizer[key];
	}
}
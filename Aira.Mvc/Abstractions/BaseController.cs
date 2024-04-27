namespace Aira.Mvc.Abstractions;

[Authorize]
public abstract class BaseController<T> : Controller
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    private SharedResourceService _sharedLocalizer;
    protected SharedResourceService Resources =>
        _sharedLocalizer ??= HttpContext.RequestServices.GetService<SharedResourceService>();

    private INotyfService _notifyInstance;
    protected INotyfService Notify
        => _notifyInstance ??= HttpContext.RequestServices.GetService<INotyfService>();

    protected void ShowSuccessInsert()
        => ShowSuccess("ALERT_SUCCESS_INSERT");
    protected void ShowSuccessUpdate()
        => ShowSuccess("ALERT_SUCCESS_UPDATE");
    protected void ShowSuccessDelete()
        => ShowSuccess("ALERT_SUCCESS_DELETE");
    protected void ShowSuccess(string key)
        => Notify.Success(Resources.GetLocalizedString(key));
}
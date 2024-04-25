namespace Aira.Api.Abstractions;

[ApiController]
[Route("[controller]")]
public class BaseApiController: ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
}
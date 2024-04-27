namespace Aira.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var context = httpContextAccessor?.HttpContext;
        Id = context?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public string Id { get; }
}
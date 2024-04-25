using System.Security.Claims;
using Aira.Application.Interfaces;

namespace Aira.Mvc.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
{
	public string Id => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
}
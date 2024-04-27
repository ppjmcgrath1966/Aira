namespace Aira.Mvc.Areas.Identity.Factories;

public class AppClaimsPrincipalFactory(
	UserManager<ApplicationUser> userManager,
	RoleManager<IdentityRole> roleManager,
	IOptions<IdentityOptions> optionsAccessor,
	ApplicationDbContext dbContext)
	: UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>(userManager, roleManager, optionsAccessor)
{
	private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

	public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
	{
		var principal = await base.CreateAsync(user);

		((ClaimsIdentity)principal.Identity!)!.AddClaims([
			new Claim(CustomClaimType.FirstName, user.FirstName ?? ""),
			new Claim(CustomClaimType.LastName, user.LastName ?? ""),
			new Claim(CustomClaimType.TenantAccess, user.TenantAccess ?? ""),
			new Claim(CustomClaimType.FullName, user.FirstName + " " + user.LastName ?? ""),
		]);

		return principal;
	}
}
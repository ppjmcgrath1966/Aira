namespace Aira.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
	[MaxLength(150)]
	public string TenantAccess { get; set; }
	[MaxLength(150)]
	public string FirstName { get; set; }
	[MaxLength(150)]
	public string LastName { get; set; }
	public byte[] ProfilePicture { get; set; }
	public int? PositionId { get; set; }
	[NotMapped]
	public string FullName => $"{FirstName} {LastName}";
}
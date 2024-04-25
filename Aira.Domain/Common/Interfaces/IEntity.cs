namespace Aira.Domain.Common.Interfaces;

public interface IEntity
{
	public int Id { get; set; }
	public bool IsDeleted { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? CreatedDateTime { get; set; }
	public string LastModifiedBy { get; set; }
	public DateTime? LastModifiedDateTime { get; set; }
}
namespace Aira.Domain.Common;

public abstract class BaseEntity : IEntity
{
	private readonly List<BaseEvent> _domainEvents = [];

	public int Id { get; set; }
	public bool IsDeleted { get; set; }
	[MaxLength(500)]
	public string CreatedBy { get; set; }
	public DateTime? CreatedDateTime { get; set; }
	[MaxLength(500)]
	public string LastModifiedBy { get; set; }
	public DateTime? LastModifiedDateTime { get; set; }

	[NotMapped]
	public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

	public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
	public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
	public void ClearDomainEvents() => _domainEvents.Clear();
}
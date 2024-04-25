namespace Aira.Application.Abstractions;

public abstract class BaseDto
{
    public int Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedDateTime { get; set; }
}
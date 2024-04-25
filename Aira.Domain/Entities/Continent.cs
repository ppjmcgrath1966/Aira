namespace Aira.Domain.Entities;

public class Continent : BaseEntity
{
    [MaxLength(2)]
    public string ContinentCode { get; set; }
    [MaxLength(500)]
    public string ContinentName { get; set; }
    [MaxLength(2)]
    public string IsoLanguageCode { get; set; }


    public ICollection<Country> Countries { get; set; }
}
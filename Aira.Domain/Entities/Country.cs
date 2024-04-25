namespace Aira.Domain.Entities;

public class Country : BaseEntity
{
    [MaxLength(2)] public string CountryCode { get; set; }
    [MaxLength(500)] public string CountryName { get; set; }
    [MaxLength(500)] public string TimeZone { get; set; }
    [MaxLength(50)] public string GmtOffset { get; set; }
    [MaxLength(10)] public string Iso3 { get; set; }
    [MaxLength(2)] public string IsoLanguageCode { get; set; }

    public Continent Continent { get; set; }

}
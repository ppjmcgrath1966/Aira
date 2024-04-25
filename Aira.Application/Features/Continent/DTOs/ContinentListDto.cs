namespace Aira.Application.Features.Continent.DTOs;

public class ContinentListDto : BaseDto
{
    [Display(Name = "NAME")]
    public string ContinentName { get; set; }
    [Display(Name = "CODE")]
    public string ContinentCode { get; set; }

    [Display(Name = "LANGUAGE")]
    public string IsoLanguageCode { get; set; }
}
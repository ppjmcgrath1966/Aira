namespace Aira.Application.Features.Continent.DTOs;

public class ContinentDto : BaseDto
{
    [Required(ErrorMessage = "VALIDATION_FIELD_REQUIRED")]
    [Display(Name = "NAME")]
    public string ContinentName { get; set; }
    [Required(ErrorMessage = "VALIDATION_FIELD_REQUIRED")]
    [Display(Name = "CODE")]
    public string ContinentCode { get; set; }
    [Required(ErrorMessage = "VALIDATION_FIELD_REQUIRED")]
    [Display(Name = "LANGUAGE")]
    public string IsoLanguageCode { get; set; }
}
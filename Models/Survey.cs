using System.ComponentModel.DataAnnotations;
namespace Verim.Frontend.Models;

public class SurveyResult
{
    [Required]public int AssetId { get; set; } 
    [Required]public int AmountStar { get; set; } 
    [Required]public int FutureStar { get; set; } 
    [Required]public bool FertilizerUse { get; set; }
    public string Comment { get; set; }
}

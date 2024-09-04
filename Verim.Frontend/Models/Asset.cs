using System.ComponentModel.DataAnnotations;
namespace Verim.Frontend.Models;

public class Asset
{
    public int AssetId {get; set;} 
    [Required(ErrorMessage = "Type field is required.")][StringLength(50)]public string? AssetType { get; set; }
    [Required(ErrorMessage = "Province field is required.")][StringLength(50)]public string? ProvinceName { get; set; }
    [Required(ErrorMessage = "District field is required.")][StringLength(50)]public string? DistrictName { get; set; } 
    [Required(ErrorMessage = "Neighborhood field is required.")][StringLength(50)]public string? NeighborhoodName { get; set; }
    [Required(ErrorMessage = "Block field is required.")][StringLength(50)]public string? BlockNumber { get; set; }
    [Required(ErrorMessage = "Parcel field is required.")][StringLength(50)]public string? ParcelNumber { get; set; }
    public string? PlantedProduct { get; set; }
}


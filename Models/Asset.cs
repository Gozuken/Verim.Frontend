using System.ComponentModel.DataAnnotations;
namespace Verim.Frontend.Models;

public class Asset
{
    public int AssetId {get; set;} 
    [Required(ErrorMessage = "Arsa tipi girilmesi zorunludur.")][StringLength(50)]public string? AssetType { get; set; }
    [Required(ErrorMessage = "İl girilmesi zorunludur.")][StringLength(50)]public string? ProvinceName { get; set; }
    [Required(ErrorMessage = "İlçe girilmesi zorunludur.")][StringLength(50)]public string? DistrictName { get; set; } 
    [Required(ErrorMessage = "Mahalle girilmesi zorunludur.")][StringLength(50)]public string? NeighborhoodName { get; set; }
    [StringLength(50)]public string? BlockNumber { get; set; }
    [Required(ErrorMessage = "Parsel Numarası girilmesi zorunludur.")][StringLength(50)]public string? ParcelNumber { get; set; }
    public string? PlantedProduct { get; set; }
    public DateOnly? PlantDate {get; set;}
    public string GetFullAdress()
    {
        return $"{ProvinceName}, {DistrictName}, {NeighborhoodName}, {ParcelNumber}.";
    }
}

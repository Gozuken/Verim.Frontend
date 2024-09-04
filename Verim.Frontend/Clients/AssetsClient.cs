using Microsoft.AspNetCore.Http.HttpResults;
using Verim.Frontend.Models;

namespace Verim.Frontend.Clients;

public class AssetsClient(HttpClient httpClient)
{

    //Creates a new asset and adds it to the list
    public async Task AddNewAssetAsync(Asset asset)
    {
        var newAsset = new Asset
        {
            AssetType = asset.AssetType,
            ProvinceName = asset.ProvinceName,
            DistrictName = asset.DistrictName,
            NeighborhoodName = asset.NeighborhoodName,
            BlockNumber = asset.BlockNumber,
            ParcelNumber = asset.ParcelNumber,
            PlantedProduct = asset.PlantedProduct
        };

        try
        {
            var response = await httpClient.PostAsJsonAsync("http://localhost:5197/main/assets", newAsset);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Asset added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add asset. Status code: {response.StatusCode}");
            }
            }
        catch (Exception ex)
        {
            Console.WriteLine($"Error on PostAsJsonAsync: {ex.Message}");
        }
    }

    public async Task<List<Asset>> GetAssetsAsync() 
    {
        try
        {
        return await httpClient.GetFromJsonAsync<List<Asset>>("http://localhost:5197/main/assets") ?? new List<Asset>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error on GetFromJsonAsync: {ex.Message}");
            return new List<Asset>(); // Fallback in case of error
        }
    }

    public async Task<Asset> GetAssetAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<Asset>($"http://localhost:5197/main/assets/{id}") ?? new Asset();
    }

    public async Task DeleteAssetAsync(int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"http://localhost:5197/main/assets/{id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Asset deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to delete asset. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error on DeleteAsync: {ex.Message}");
        }
    }

    public async Task UpdateAssetAsync(Asset asset)
    {
        var response = await httpClient.PutAsJsonAsync<Asset>($"http://localhost:5197/main/assets/{asset.AssetId}", asset);
        try
        {
        if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Asset updated successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to update asset. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error on GetFromJsonAsync: {ex.Message}");
        }
    }

    public async Task<ComboData> GetComboOptions()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<ComboData>("http://localhost:5197/main/assets/options")
                    ?? throw new InvalidOperationException("The data returned from the API is null.");
        }
        catch(Exception ex)
        {
            throw new ApplicationException("Error fetching ComboData : ", ex);
        }
    }
}

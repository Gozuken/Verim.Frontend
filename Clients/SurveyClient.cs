using System.Globalization;
using System.Text.Json;
using Verim.Frontend.Models;
namespace Verim.Frontend.Clients;

public class SurveyClient(HttpClient httpClient)
{
    public async Task AddSurveyResult(SurveyResult surveyResult)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync($"http://localhost:5197/main/survey/{surveyResult.AssetId}", surveyResult);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Survey sent successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to send survey. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error on, SurveyClient PostAsJsonAsync: {ex.Message}");
        }
    }
}
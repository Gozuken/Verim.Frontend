using System.Globalization;
using System.Text.Json;
using Verim.Frontend.Models;
namespace Verim.Frontend.Clients;

public class WeatherClient(HttpClient httpClient)
{
    public async Task<List<WeatherData>> GetWeatherDataAsync()
    {
        return new List<WeatherData>
        {
            new WeatherData { Day = "09-20", Amount = (double)10.5M, SecondAmount = (double)10.5M },
            new WeatherData { Day = "09-21", Amount = (double)0.0M, SecondAmount = (double)10.5M},
            new WeatherData { Day = "09-22", Amount = (double)5.0M, SecondAmount = (double)10.5M},
            new WeatherData { Day = "09-23", Amount = (double)2.5M, SecondAmount = (double)10.5M},
            new WeatherData { Day = "09-24", Amount = (double)2.0M, SecondAmount = (double)10.5M},
            new WeatherData { Day = "09-25", Amount = (double)0.0M, SecondAmount = (double)10.5M},
            new WeatherData { Day = "09-26", Amount = (double)3.5M, SecondAmount = (double)10.5M}, 
            new WeatherData { Day = "09-27", Amount = (double)3.5M, SecondAmount = (double)10.5M},
            new WeatherData { Day = "09-28", Amount = (double)9.5M, SecondAmount = (double)10.5M},
            new WeatherData { Day = "09-29", Amount = (double)9.5M, SecondAmount = (double)10.5M}
        };
    }     

    public async Task<List<WeatherData>> GetDataFromMeteoAsync(Asset asset)
    {
        var (lati, longi) = await GetCoordinateAsync(asset);
        string latitude = Uri.EscapeDataString(lati.ToString("F6", CultureInfo.InvariantCulture));
        string longitude = Uri.EscapeDataString(longi.ToString("F6", CultureInfo.InvariantCulture));
        var meteoRainLink = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=rain_sum&past_days=3";
        //https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude=13.41&daily=temperature_2m_max,temperature_2m_min&timezone=auto&past_days=3


        var meteoRainResponse = await httpClient.GetStringAsync(meteoRainLink);
        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(meteoRainResponse);


        var daily = jsonResponse.GetProperty("daily");
        var time = daily.GetProperty("time").EnumerateArray();
        var rainSum = daily.GetProperty("rain_sum").EnumerateArray();


        var weatherDataList = new List<WeatherData>();

        using (var timeEnumerator = time.GetEnumerator())
        using (var rainSumEnumerator = rainSum.GetEnumerator())
        {
            while (timeEnumerator.MoveNext() && rainSumEnumerator.MoveNext())
            {
                weatherDataList.Add(new WeatherData
                {
                    Day = timeEnumerator.Current.GetString(),
                    Amount = rainSumEnumerator.Current.GetDouble()
                });
            }
        }

        return weatherDataList; // Return the list of WeatherData
    } 

    public async Task<List<WeatherData>> GetTempDataFromMeteo(Asset asset)
    {
        var (lati, longi) = await GetCoordinateAsync(asset);
        string latitude = Uri.EscapeDataString(lati.ToString("F6", CultureInfo.InvariantCulture));
        string longitude = Uri.EscapeDataString(longi.ToString("F6", CultureInfo.InvariantCulture));
        var meteoRainLink = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min&timezone=auto&past_days=3";

        var meteoRainResponse = await httpClient.GetStringAsync(meteoRainLink);
        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(meteoRainResponse);

        var daily = jsonResponse.GetProperty("daily");
        var time = daily.GetProperty("time").EnumerateArray();
        var MaxTemperature = daily.GetProperty("temperature_2m_max").EnumerateArray();
        var MinTemperature = daily.GetProperty("temperature_2m_min").EnumerateArray();

        var weatherDataList = new List<WeatherData>();

        using (var timeEnumerator = time.GetEnumerator())
        using (var maxTempEnumerator = MaxTemperature.GetEnumerator())
        using (var minTempEnumerator = MinTemperature.GetEnumerator())
        {
            while (timeEnumerator.MoveNext() && maxTempEnumerator.MoveNext() && minTempEnumerator.MoveNext())
            {
                weatherDataList.Add(new WeatherData
                {
                    Day = timeEnumerator.Current.GetString(),
                    Amount = maxTempEnumerator.Current.GetDouble(),
                    SecondAmount = minTempEnumerator.Current.GetDouble()
                });
            }
        }

        return weatherDataList;
    }



    public async Task<(double, double)> GetCoordinateAsync(Asset asset)
    {
        string api_key = "66f27b790fa67519911627cgvd5846e";
        string encodedAddress = Uri.EscapeDataString(asset.GetFullAdress());
        
        var response = await httpClient.GetFromJsonAsync<List<GeocodeResponse>>(
            $"https://geocode.maps.co/search?q={encodedAddress}&api_key={api_key}");

        if (response is not null && response.Any())
        {
            var firstResult = response.First();
            // Parse the lat and lon strings to doubles
            if (double.TryParse(firstResult.lat, NumberStyles.Any, CultureInfo.InvariantCulture, out double latitude)
                && double.TryParse(firstResult.lon, NumberStyles.Any, CultureInfo.InvariantCulture, out double longitude))
            {
                return (latitude, longitude);
            }
        }
        return (0.0, 0.0);
    }
}

public class GeocodeResponse
{
    public string lat { get; set; }
    public string lon { get; set; }
}
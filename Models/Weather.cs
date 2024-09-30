using System;

namespace Verim.Frontend.Models;

public class WeatherData
{
    public string Day { get; set; }
    public double Amount { get; set; }
    public double? SecondAmount { get; set; }
}


public class Charts
{
    public List<WeatherData> Sun { get; set; }      
    public List<WeatherData> Rain { get; set; } 
    public List<WeatherData> MaxTemp { get; set; } 
    public List<WeatherData> MinTemp { get; set; } 
}

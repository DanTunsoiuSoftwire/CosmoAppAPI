using System.Globalization;
using System.Text.Json;
using CosmoAppAPI.Models.ResponseObjects;
using CosmoAppAPI.Services.Abstractions;

namespace CosmoAppAPI.Services.Implementations;

public class APODServices : IAPODServices
{
    private static readonly HttpClient HttpClient = new HttpClient()
    {
        BaseAddress = new Uri("https://api.nasa.gov"),
    };
    private readonly IConfiguration _config;

    public APODServices(IConfiguration config)
    {
        _config = config;
    }
    
    public async Task<APODResponse> GetPicture()
    {
        HttpResponseMessage response = await HttpClient.GetAsync("planetary/apod?api_key=" + _config["NASA:ServiceApiKey"]);

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            return new APODResponse();
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<APODResponse>(jsonResponse);
    }

    public async Task<APODResponse> GetPictureByDate(DateOnly date)
    {
        HttpResponseMessage response;

        try
        {
            response = await HttpClient.GetAsync("planetary/apod?api_key=" + _config["NASA:ServiceApiKey"] +
                                                 "&date=" + date.ToString("o",CultureInfo.InvariantCulture));
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            return new APODResponse();
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<APODResponse>(jsonResponse);
    }
}

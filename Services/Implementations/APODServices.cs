using System.Globalization;
using System.Text.Json;
using CosmoAppAPI.Models.ResponseObjects;
using CosmoAppAPI.Services.Abstractions;

namespace CosmoAppAPI.Services.Implementations;

public class APODServices : IAPODServices
{
    private static HttpClient _httpClient = null!;
    private readonly IConfiguration _config;

    public APODServices(IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _config = config;
        if (_httpClient == null!)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.nasa.gov");
        }
    }
    
    public async Task<APODResponse> GetPicture()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("planetary/apod?api_key=" + _config["NASA:ServiceApiKey"]);

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
            response = await _httpClient.GetAsync("planetary/apod?api_key=" + _config["NASA:ServiceApiKey"] +
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

using System.Globalization;
using System.Text.Json;
using CosmoAppAPI.Models.ResponseObjects;
using CosmoAppAPI.Services.Abstractions;

namespace CosmoAppAPI.Services.Implementations;

public class APODServices : IAPODServices
{
    private static HttpClient _httpClient = null!;
    private readonly IConfiguration _config;

    public APODServices(IConfiguration config, HttpClient httpClient)
    {
        _config = config;
        if (_httpClient == null!)
        {
            _httpClient = httpClient;
        }
    }
    
    public async Task<APODResponse> GetPicture()
    {
        HttpResponseMessage response;

        try
        {
            response = await _httpClient.GetAsync("planetary/apod?api_key=" + _config["NASA:ServiceApiKey"]);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            return new APODResponse();
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<APODResponse>(jsonResponse)!;
    }

    public async Task<ImageResponse> GetPictureByDate(DateOnly date)
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
            return new ImageResponse();
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();

        APODResponse apodResponse = JsonSerializer.Deserialize<APODResponse>(jsonResponse)!;
        ImageResponse imageResponse = new ImageResponse();
        imageResponse.Url = apodResponse.url;
        imageResponse.Title = apodResponse.title;
        return imageResponse;
    }
}

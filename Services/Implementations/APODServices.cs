using System.Text.Json;
using CosmoAppAPI.Models.ResponseObjects;
using CosmoAppAPI.Services.Abstractions;

namespace CosmoAppAPI.Services.Implementations;

public class APODServices : IAPODServices
{
    private readonly HttpClient _httpClient;

    public APODServices()
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://api.nasa.gov"),
        };
    }
    
    public async Task<APODResponse> GetPicture()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("planetary/apod?api_key=t2o7TXGVL7dTDhrXbqIkce2UEfmtn7D3APzFEdDB");

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
}

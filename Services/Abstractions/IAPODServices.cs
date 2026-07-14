using CosmoAppAPI.Models.ResponseObjects;

namespace CosmoAppAPI.Services.Abstractions;

public interface IAPODServices
{
    public Task<APODResponse> GetPicture();
    public Task<APODResponse> GetPictureByDate(DateOnly date);
}

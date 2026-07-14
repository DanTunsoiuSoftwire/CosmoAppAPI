using Microsoft.AspNetCore.Mvc;
using CosmoAppAPI.Models.ResponseObjects;
using CosmoAppAPI.Services.Abstractions;

namespace CosmoAppAPI.Controllers;

[ApiController]
[Route("apod")]
public class APODController : ControllerBase
{
    private readonly IAPODServices _apodServices;
    
    public APODController(IAPODServices apodServices)
    {
            _apodServices = apodServices;
    }
    
    [HttpGet("today")]
    public async Task<APODResponse> Get()
    {
        APODResponse apodResponse = await _apodServices.GetPicture();

        if (apodResponse.url == null!)
        {
            this.HttpContext.Response.StatusCode = 404;
        }
        
        return apodResponse;
    }
}

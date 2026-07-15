using System.Globalization;
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
    public async Task<IActionResult> Get()
    {
        APODResponse apodResponse = await _apodServices.GetPicture();

        if (apodResponse.url == null!)
        {
            return NotFound("No picture found.");
        }
        
        return Ok(apodResponse);
    }

    [HttpGet("photos/{date}")]
    public async Task<IActionResult> GetPhoto([FromRoute] DateOnly date)
    {
        ImageResponse imageResponse = await _apodServices.GetPictureByDate(date);
        
        if (imageResponse.Url == null!)
        {
            return NotFound("No picture found.");
        }
        
        return Ok(imageResponse);
    }
}

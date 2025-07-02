using FoodTruck.Api.Interfaces;
using FoodTruck.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodTruckController : ControllerBase
{
    private readonly IFoodTruckService _service;

    public FoodTruckController(FoodTruckService service)
    {
        _service = service;
    }

    /// <summary>
    /// Gets food trucks near a given location.
    /// </summary>
    /// <param name="lat">Latitude of user location</param>
    /// <param name="lng">Longitude of user location</param>
    /// <returns>List of nearby food trucks</returns>
    /// <response code="200">Returns list of nearby food trucks</response>
    [HttpGet]
    public async Task<IActionResult> Get(double lat, double lng, [FromQuery] List<string>? categories = null)
    {
        var results = await _service.GetNearbyTrucksAsync(lat, lng, categories);
        return Ok(results);
    }
}

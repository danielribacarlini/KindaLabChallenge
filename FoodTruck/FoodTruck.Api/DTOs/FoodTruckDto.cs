namespace FoodTruck.Api.DTOs;

public class FoodTruckDto
{
    public string? Applicant { get; set; }
    public string? FacilityType { get; set; }
    public string? LocationDescription { get; set; }
    public string? Address { get; set; }
    public string FoodItems { get; set; }
    public List<string>? FoodItemsList { get; set; } = new();
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}

using FoodTruck.Api.DTOs;

namespace FoodTruck.Api.Interfaces
{
    public interface IFoodTruckService
    {
        Task<IEnumerable<FoodTruckDto>> GetNearbyTrucksAsync(double lat, double lng, IEnumerable<string>? categories = null, double radiusKm = 10.0);
    }
}

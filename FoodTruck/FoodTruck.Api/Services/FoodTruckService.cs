using System.Net.Http.Json;
using FoodTruck.Api.Constants;
using FoodTruck.Api.DTOs;
using FoodTruck.Api.Interfaces;

namespace FoodTruck.Api.Services;

public class FoodTruckService : IFoodTruckService
{
    private readonly HttpClient _httpClient;

    public FoodTruckService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<FoodTruckDto>> GetNearbyTrucksAsync(double lat, double lng, IEnumerable<string>? categories = null, double radiusKm = 100.0)
    {
        var all = await _httpClient.GetFromJsonAsync<List<FoodTruckDto>>("https://data.sfgov.org/resource/rqzj-sfat.json");

        if (all == null)
            return Enumerable.Empty<FoodTruckDto>();

        // Siempre llenamos FoodItemsList
        foreach (var t in all)
        {
            if (!string.IsNullOrWhiteSpace(t.FoodItems))
            {
                t.FoodItemsList = t.FoodItems
                    .ToLowerInvariant()
                    .Split(new[] { ',', ':', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => i.Trim())
                    .ToList();
            }
            else
            {
                t.FoodItemsList = new List<string>();
            }
        }

        // Filtro de proximidad
        var nearby = all.Where(t =>
            t.Latitude.HasValue && t.Longitude.HasValue &&
            GetDistanceKm(lat, lng, t.Latitude.Value, t.Longitude.Value) <= radiusKm
        );

        // Filtro por categorías
        if (categories != null && categories.Any())
        {
            var categorySet = new HashSet<string>(categories, StringComparer.OrdinalIgnoreCase);

            nearby = nearby.Where(t =>
            {
                if (t.FoodItemsList == null || t.FoodItemsList.Count == 0)
                    return false;

                var itemCategories = t.FoodItemsList
                    .Where(i => Categories._categoryMapping.ContainsKey(i))
                    .Select(i => Categories._categoryMapping[i]);

                return itemCategories.Any(cat => categorySet.Contains(cat));
            });
        }

        return nearby;
    }
    private double GetDistanceKm(double lat1, double lon1, double lat2, double lon2)
    {
        var R = 6371; // Radio de la Tierra en km
        var dLat = DegreesToRadians(lat2 - lat1);
        var dLon = DegreesToRadians(lon2 - lon1);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private double DegreesToRadians(double deg) => deg * (Math.PI / 180);
}

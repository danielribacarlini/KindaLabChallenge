using System.Data;
using FoodTruck.Api.Constants;
using FoodTruck.Api.DTOs;
using FoodTruck.Api.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace FoodTruck.Api.Services;

public class FoodTruckService : IFoodTruckService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;

    public FoodTruckService(HttpClient httpClient, IMemoryCache memoryCache)
    {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<FoodTruckDto>> GetNearbyTrucksAsync(
        double lat,     
        double lng,     
        IEnumerable<string>? categories = null, 
        double radiusKm = 10.0)
    {
        // Try to get the cache
        var all = await GetOrFetchFoodTrucksAsync();

        // Proximity filter
        var nearby = all.Where(t =>
            t.Latitude.HasValue && t.Longitude.HasValue &&
            GetDistanceKm(lat, lng, t.Latitude.Value, t.Longitude.Value) <= radiusKm
        );

        // Category filter
        if (categories != null && categories.Any())
        {
            var categorySet = new HashSet<string>(categories, StringComparer.OrdinalIgnoreCase);

            nearby = nearby.Where(t =>
            {
                if (t.FoodItemsList == null || t.FoodItemsList.Count == 0)
                    return false;

                var itemCategories = t.FoodItemsList
                    .Where(i => Categories.CategoryMapping.ContainsKey(i))
                    .Select(i => Categories.CategoryMapping[i]);

                return itemCategories.Any(cat => categorySet.Contains(cat));
            });
        }

        return nearby;
    }

    private async Task<List<FoodTruckDto>> GetOrFetchFoodTrucksAsync()
    {
        if (_memoryCache.TryGetValue(ServiceConstants.FoodTrucksCacheKey, out List<FoodTruckDto>? cachedList))
        {
            return cachedList!;
        }

        var all = await _httpClient.GetFromJsonAsync<List<FoodTruckDto>>(ServiceConstants.SFFoodTrucksApiUrl);

        if (all == null)
            return new List<FoodTruckDto>();

        foreach (var t in all)
        {
            t.FoodItemsList = string.IsNullOrWhiteSpace(t.FoodItems)
                ? new List<string>()
                : t.FoodItems
                    .ToLowerInvariant()
                    .Split(new[] { ',', ':', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => i.Trim())
                    .ToList();
        }

        _memoryCache.Set(ServiceConstants.FoodTrucksCacheKey, all);
        return all;
    }

    private double GetDistanceKm(double lat1, double lon1, double lat2, double lon2)
    {
        var R = 6371; // Earth radius in Km
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

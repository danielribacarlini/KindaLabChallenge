using System.Net;
using System.Net.Http.Json;
using FoodTruck.Api.Constants;
using FoodTruck.Api.DTOs;
using FoodTruck.Api.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.Protected;
using Xunit;

public class FoodTruckServiceTests
{
    private HttpClient CreateMockHttpClient(List<FoodTruckDto> data)
    {
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(data),
            });

        return new HttpClient(handlerMock.Object) { BaseAddress = new Uri("https://data.sfgov.org") };
    }

    [Fact]
    public async Task GetNearbyTrucksAsync_FiltersByDistance()
    {
        var mockData = new List<FoodTruckDto>
        {
            new() { Latitude = 37.7749, Longitude = -122.4194, FoodItems = "tacos" }, // inside radius
            new() { Latitude = 34.0522, Longitude = -118.2437, FoodItems = "tacos" } // outside radius
        };

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var service = new FoodTruckService(CreateMockHttpClient(mockData), memoryCache);

        var result = await service.GetNearbyTrucksAsync(37.7749, -122.4194, null, 50);

        Assert.Single(result);
    }

    [Fact]
    public async Task GetNearbyTrucksAsync_FiltersByCategory()
    {
        var mockData = new List<FoodTruckDto>
        {
            new() { Latitude = 37.7749, Longitude = -122.4194, FoodItems = "tacos" },
            new() { Latitude = 37.7750, Longitude = -122.4195, FoodItems = "coffee" }
        };

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var service = new FoodTruckService(CreateMockHttpClient(mockData), memoryCache);

        var result = await service.GetNearbyTrucksAsync(37.7749, -122.4194, new[] { "Mexican Food" }, 10);

        Assert.Single(result);
        Assert.Contains(result, t => t.FoodItems!.Contains("tacos"));
    }

    [Fact]
    public async Task GetNearbyTrucksAsync_ExcludesWhenNoCategoryMatch()
    {
        var mockData = new List<FoodTruckDto>
        {
            new() { Latitude = 37.7749, Longitude = -122.4194, FoodItems = "espresso" }
        };

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var service = new FoodTruckService(CreateMockHttpClient(mockData), memoryCache);

        var result = await service.GetNearbyTrucksAsync(37.7749, -122.4194, new[] { "Mexican Food" }, 10);

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetNearbyTrucksAsync_IgnoresNullLatLng()
    {
        var mockData = new List<FoodTruckDto>
        {
            new() { Latitude = null, Longitude = -122.4194, FoodItems = "tacos" },
            new() { Latitude = 37.7749, Longitude = null, FoodItems = "tacos" },
            new() { Latitude = null, Longitude = null, FoodItems = "tacos" },
        };

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var service = new FoodTruckService(CreateMockHttpClient(mockData), memoryCache);

        var result = await service.GetNearbyTrucksAsync(37.7749, -122.4194, null, 10);

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetNearbyTrucksAsync_IgnoresEmptyFoodItems()
    {
        var mockData = new List<FoodTruckDto>
        {
            new() { Latitude = 37.7749, Longitude = -122.4194, FoodItems = null },
            new() { Latitude = 37.7750, Longitude = -122.4194, FoodItems = string.Empty }
        };

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var service = new FoodTruckService(CreateMockHttpClient(mockData), memoryCache);

        var result = await service.GetNearbyTrucksAsync(37.7749, -122.4194, new[] { "Mexican Food" }, 10);

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetNearbyTrucksAsync_UsesCachedDataOnSecondCall()
    {
        var mockData = new List<FoodTruckDto>
        {
            new() { Latitude = 37.7749, Longitude = -122.4194, FoodItems = "tacos" }
        };

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var client = CreateMockHttpClient(mockData);
        var service = new FoodTruckService(client, memoryCache);

        var first = await service.GetNearbyTrucksAsync(37.7749, -122.4194);
        var second = await service.GetNearbyTrucksAsync(37.7749, -122.4194);

        Assert.Single(first);
        Assert.Single(second);
    }
}

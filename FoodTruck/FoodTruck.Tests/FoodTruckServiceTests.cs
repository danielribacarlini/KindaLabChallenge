using FoodTruck.Api.DTOs;
using FoodTruck.Api.Services;

namespace FoodTruck.Tests
{
    public class FoodTruckServiceTests
    {
        [Fact]
        public async Task GetNearbyTrucksAsync_FiltersByDistance()
        {
            // Arrange
            var mockData = new List<FoodTruckDto>
        {
            new() { Applicant = "Near Truck", Latitude = 37.7749, Longitude = -122.4194 },
            new() { Applicant = "Far Truck", Latitude = 34.0522, Longitude = -118.2437 } // LA
        };

            var handler = new MockHttpMessageHandler(mockData);
            var client = new HttpClient(handler);

            var service = new FoodTruckService(client);

            // Act
            var results = await service.GetNearbyTrucksAsync(37.7749, -122.4194, radiusKm: 5);

            // Assert
            Assert.Single(results);
            Assert.Equal("Near Truck", results.First().Applicant);
        }
    }
}


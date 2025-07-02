using System.Net.Http.Json;
using System.Net;

namespace FoodTruck.Tests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly object _response;

        public MockHttpMessageHandler(object response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var msg = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(_response)
            };
            return Task.FromResult(msg);
        }
    }
}

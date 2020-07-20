using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreRateLimit.Tests
{
    public class GeneralRateLimitTests: IClassFixture<RateLimitWebApplicationFactory>
    {
        private const string apiParentChildValuesPath = "/api/parentchild";
     
        private readonly HttpClient _client;

        public GeneralRateLimitTests(RateLimitWebApplicationFactory factory)
        {
            _client = factory.CreateClient(options: new WebApplicationFactoryClientOptions
            {
                BaseAddress = new System.Uri("https://localhost:44304")
            });
        }

        [Fact]
        public async Task GlobalIpLimitZeroRule()
        {
            var ip = "13.247.22.231";

            int responseStatusCode = 0;

            for (int i = 0; i < 4; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, apiParentChildValuesPath + "/parent");
                request.Headers.Add("X-Real-IP", ip);
                request.Headers.Add("X-ClientId", Guid.NewGuid().ToString("N"));

                var response = await _client.SendAsync(request);
                responseStatusCode = (int)response.StatusCode;
            }

            Assert.Equal(200, responseStatusCode);
        }
    }
}
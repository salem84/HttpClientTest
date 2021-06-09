using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HttpClientTest
{
    public class HttpClientTest
    {
        [Fact]
        public void Test_Work()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<Service>();
            serviceCollection.AddHttpClient<Service>(options =>
            {
                options.BaseAddress = new Uri("https://petstore.swagger.io/v2/");
            });

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Act
            var s = serviceProvider.GetRequiredService<Service>();

            // Assert
            var baseAddress = s.httpClient.BaseAddress;
            Assert.NotNull(baseAddress);
        }

        [Fact]
        public async Task Test_Not_Work_With_Inverted_Registration()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddHttpClient<Service>(options =>
            {
                options.BaseAddress = new Uri("https://petstore.swagger.io/v2/");
            });

            serviceCollection.AddTransient<Service>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Act
            var s = serviceProvider.GetRequiredService<Service>();

            // Assert
            var baseAddress = s.httpClient.BaseAddress;
            Assert.Null(baseAddress);

            var result = await s.Send();
            Assert.False(result);
        }
    }
}

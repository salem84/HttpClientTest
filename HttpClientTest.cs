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
            var services = new ServiceCollection();

            services.AddTransient<SampleService>();
            services.AddHttpClient<SampleService>(options =>
            {
                options.BaseAddress = new Uri("https://petstore.swagger.io/v2/");
            });

            var serviceProvider = services.BuildServiceProvider();

            // Act
            var s = serviceProvider.GetRequiredService<SampleService>();

            // Assert
            var baseAddress = s.httpClient.BaseAddress;
            Assert.NotNull(baseAddress);
        }

        [Fact]
        public async Task Test_Not_Work_With_Inverted_Registration()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddHttpClient<SampleService>(options =>
            {
                options.BaseAddress = new Uri("https://petstore.swagger.io/v2/");
            });
            services.AddTransient<SampleService>();

            var serviceProvider = services.BuildServiceProvider();

            // Act
            var s = serviceProvider.GetRequiredService<SampleService>();

            // Assert (here baseaddress is null)
            var baseAddress = s.httpClient.BaseAddress;
            Assert.Null(baseAddress);

            var result = await s.Send();
            Assert.False(result);
        }
    }
}

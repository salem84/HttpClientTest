using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientTest
{
    class SampleService
    {
        public readonly HttpClient httpClient;

        public SampleService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> Send()
        {
            try
            {
                //httpClient.BaseAddress = new Uri("https://petstore.swagger.io/v2/");
                var result = await httpClient.GetAsync("/pets/1");
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}

using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
//using System.Text.Json;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;
using Xunit;

namespace XFunctionalTests.APIs
{
    public class EmployeeAPIs: IClassFixture<APIConfig>
    {
        public HttpClient Client { get; }   

        public EmployeeAPIs(APIConfig factory)
        {
            Client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsItemGivenValidId()
        {
                var token = ApiTokenHelper.getToken();
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await Client.GetAsync("api/employee");
                response.EnsureSuccessStatusCode();

                var stringResponse = await response.Content.ReadAsStringAsync();

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DescriptiveResponse<List<Employee>>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

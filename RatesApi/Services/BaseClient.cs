using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NLog;
using RatesApi.Services.Interface;
using RestSharp;
using System.Net;

namespace RatesApi.Services
{
    public class BaseClient : IBaseClient
    {
        private readonly ILogger<BaseClient> _logger;
        public BaseClient(ILogger<BaseClient> logger)
        {
            _logger = logger;
        }
        public async Task<RestResponse> GetResponseSourse(string url)
        {
                        
                var client = new RestClient(url);
                var request = new RestRequest();
                var response = await client.GetAsync(request);
                //if (response.StatusCode == HttpStatusCode.OK)
                //{
                //    json = JObject.Parse(response.Content);
                //}
                //_logger.LogDebug("Received a responce from the sourse");
                return response;           
 
        }
    }
}

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
        public async Task<JObject> GetResponseSourse(string url)
        {
            JObject json = null;
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest();
                var response = await client.GetAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    json = JObject.Parse(response.Content);
                }
                _logger.LogDebug("Received a responce from the sourse");
                return json;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get a response from the source", ex);
                throw new Exception();
            }
        }
    }
}

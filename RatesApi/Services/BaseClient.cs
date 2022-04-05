using Newtonsoft.Json.Linq;
using NLog;
using RatesApi.Services.Interface;
using RestSharp;
using System.Net;

namespace RatesApi.Services
{
    public class BaseClient : IBaseClient
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

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
                _logger.Debug("Received a responce from the sourse");
                return json;
            }
            catch (Exception ex)
            {
                _logger.Error("No reseived a responce from the sourse", ex);
                throw new Exception();
            }
        }
    }
}

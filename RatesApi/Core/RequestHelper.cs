using Marvelous.Contracts.Enums;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesApi.Core
{
    public class RequestHelper : IRequestHelper
    {
        public async Task<RestResponse<T>> SendRequest<T>(string url, string path, string jwtToken = "null")
        {
            var request = new RestRequest(path);
            var client = new RestClient(url);
            client.Authenticator = new JwtAuthenticator(jwtToken);
            client.AddDefaultHeader(nameof(Microservice), Microservice.MarvelousRatesApi.ToString());
            var response = await client.ExecuteAsync<T>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception();
            }
            return response;
        }
    }
}

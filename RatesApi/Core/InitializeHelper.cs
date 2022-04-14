using Marvelous.Contracts.Endpoints;
using Marvelous.Contracts.ResponseModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RatesApi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesApi.Core
{
    public class InitializeHelper : IInitializeHelper
    {
        private IRequestHelper _requestHelper;
        private ILogger<InitializeHelper> _logger;
        //private IMemoryCache _cache;
        private IConfiguration _configuration;

        public InitializeHelper(IRequestHelper requestHelper, ILogger<InitializeHelper> logger, IConfiguration configuration)
        {
            _requestHelper = requestHelper;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task InitializeConfig()
        {
            var token = await _requestHelper.SendRequest<string>("https://piter-education.ru:6042/", AuthEndpoints.ApiAuth + AuthEndpoints.TokenForMicroservice);
            var configs = await _requestHelper.SendRequest<IEnumerable<ConfigResponseModel>>("https://piter-education.ru:6040/", ConfigsEndpoints.Configs, token.Data);
            foreach (var config in configs.Data)
            {
                _configuration[config.Key] =  config.Value;
            }
        }
    }
}

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Common.Discovery;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FortuneTeller.UI.Services
{
    public class FortuneServiceClient : IFortuneService
    {
        private readonly ILogger<FortuneServiceClient> _logger;
        private IOptions<FortuneServiceOptions> _config;
        private readonly HttpClient _httpClient;
        private readonly DiscoveryHttpClientHandler _handler;

        private FortuneServiceOptions Config
        {
            get
            {
                return _config.Value;
            }
        }

        public FortuneServiceClient(
            HttpClient httpClient,
            IOptions<FortuneServiceOptions> config, 
            ILogger<FortuneServiceClient> logger,
            IDiscoveryClient client)
        {
            _logger = logger;
            _config = config;
            _httpClient = httpClient;
            _handler = new DiscoveryHttpClientHandler(client);
        }

        public async Task<List<Fortune>> AllFortunesAsync()
        {
            var client = new HttpClient(_handler, false);
            var response = await client.GetAsync(Config.AllFortunesURL);
            return await response.Content.ReadAsAsync<List<Fortune>>();

        }

        public async Task<Fortune> RandomFortuneAsync()
        {
            var client = new HttpClient(_handler, false);
            var result = await client.GetAsync(Config.RandomFortuneURL);
            return await result.Content.ReadAsAsync<Fortune>();
        }
    }
}
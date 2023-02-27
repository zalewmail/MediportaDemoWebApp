using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MediportaDemoWebApp
{
    public class StackOverflowHttpClient
    {
        private readonly HttpClient _httpClient;

        public StackOverflowHttpClient()
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            _httpClient.BaseAddress = new Uri("https://api.stackexchange.com/2.3/");
            _httpClient.Timeout = new TimeSpan(0, 0, 0, 5);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> GetTagsAsync(int page) =>
            await _httpClient.GetAsync($"tags?page={page}&pagesize=100&order=desc&sort=popular&site=stackoverflow");
    }
}

using MediportaDemoWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediportaDemoWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly StackOverflowHttpClient _stackOverflowHttpClient;

        public HomeController(StackOverflowHttpClient stackOverflowHttpClient)
        {
            _stackOverflowHttpClient = stackOverflowHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<Tag> allTags = new List<Tag>();
            int totalTagsUsageCount = 0;

            for (int i = 1; i <= 10; i++)
            {
                HttpResponseMessage response = await _stackOverflowHttpClient.GetTagsAsync(i);
                response.EnsureSuccessStatusCode();
                string responseJSON = await response.Content.ReadAsStringAsync();
                var tags = JsonSerializer.Deserialize<Tags>(responseJSON);
                allTags.AddRange(tags.Items);
                if (!tags.isMore) break;
            }
            
            foreach (var tag in allTags)
            {
                totalTagsUsageCount += tag.Count; 
            }

            foreach (var tag in allTags)
            {
                tag.Popularity = (tag.Count * 100 / (double)totalTagsUsageCount);
            }

            return View(allTags);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

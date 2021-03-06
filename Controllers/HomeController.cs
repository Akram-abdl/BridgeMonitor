using BridgeMonitor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BridgeMonitor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var closures = GetAllBridgesClosureFromApi();
            var today = DateTime.Now;

            closures.RemoveAll(closure => closure.ReopeningDate < today);
            return View(closures[0]);
        }

        
        public IActionResult AllClosures()
        {
            var closures = GetAllBridgesClosureFromApi();
            var nextclosure = closures[1];
            return View(closures);
        }


        public IActionResult Details(int InstanceID)
        {
            var closures = GetAllBridgesClosureFromApi();
            
            return View(closures[InstanceID]);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
        private static List<BridgeClosure> GetAllBridgesClosureFromApi()
        {
            
            using (var client = new HttpClient())
            {
               
                var response = client.GetAsync("https://api.alexandredubois.com/pont-chaban/api.php");
                var stringResult = response.Result.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<BridgeClosure>>(stringResult.Result);
                var sortedresult = result.OrderBy(closing => closing.ClosingDate).ToList();

                return sortedresult;
                


            }
        }
    }
}

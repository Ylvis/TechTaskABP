using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using TecnhicalTask.UI.Models;
using TechicalTask.API.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TecnhicalTask.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory _httpClientFactory)
        {
            _logger = logger;
            httpClientFactory = _httpClientFactory;
        }

        [HttpGet]
        public IActionResult MainMenu()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FirstExperiment()
        {
            id = 1;

            var request = httpClientFactory.CreateClient();
            var requestToIpify = await request.GetAsync("https://api.ipify.org");
            IPv4 = await requestToIpify.Content.ReadAsStringAsync();

            byte[] inputBytes = Encoding.UTF8.GetBytes(IPv4);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                IPv4 = BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
            }

            var apiClient = httpClientFactory.CreateClient();
            var url = "https://localhost:7078/api/Result/PassExperiment1?hash=" + IPv4 + "&id=" + id + "&button_color=null";
            var response = await apiClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            ViewBag.Value = content;
            return View();
        }

        int id;
        string IPv4;
        [HttpPost]
        public async Task<IActionResult> FirstExperiment(string button_color)
        {
            id = 1;

            var request = httpClientFactory.CreateClient();
            var requestToIpify = await request.GetAsync("https://api.ipify.org");
            IPv4 = await requestToIpify.Content.ReadAsStringAsync();

            byte[] inputBytes = Encoding.UTF8.GetBytes(IPv4);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                IPv4 = BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();               
            }
            
            var apiClient = httpClientFactory.CreateClient();
            var url = "https://localhost:7078/api/Result/PassExperiment1?hash=" + IPv4 + "&id=" + id + "&button_color=" + button_color;
            var response = await apiClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            ViewBag.Value = content;
            return View("FirstExperiment");
        }

        public async Task<IActionResult> SecondExperiment()
        {
            int id = 2;

            var request = httpClientFactory.CreateClient();
            var requestToIpify = await request.GetAsync("https://api.ipify.org");
            var IPv4 = await requestToIpify.Content.ReadAsStringAsync();

            byte[] inputBytes = Encoding.UTF8.GetBytes(IPv4);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                IPv4 = BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
            }

            var apiClient = httpClientFactory.CreateClient();
            var url = "https://localhost:7078/api/Result/PassExperiment2?hash=" + IPv4 + "&id=" + id;
            var response = await apiClient.GetAsync(url);
            ViewBag.Value = response.Content.ReadAsStringAsync().Result;

            return View();
        }

        [HttpGet]
        public IActionResult StatisticPage() => View();

        [HttpPost]
        public async Task<IActionResult> StatisticPage(string key)
        {
            var apiClient = httpClientFactory.CreateClient();
            string url = "https://localhost:7078/api/Result/GetByXName/" + key;
            var response = await apiClient.GetAsync(url);
            string experiments = await response.Content.ReadAsStringAsync();


            //Create view model
            List<Result> results = new List<Result>();
            results = JsonConvert.DeserializeObject<List<Result>>(experiments);
            ViewBag.Count = results.Count();

            return View(results);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using VueProject.DTO;
using VueProject.Models;

namespace VueProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext _northwindContext;


        public HomeController(ILogger<HomeController> logger, NorthwindContext northwindContext)
        {
            _logger = logger;
            _northwindContext = northwindContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Basic()
        {
            return View();
        }
        public IActionResult Method()
        {
            return View();
        }
        public IActionResult Exchange()
        {
            return View();
        }

        public IActionResult Once()
        {
            return View();
        }

        public IActionResult EnableButton()
        {
            return View();
        }

        public IActionResult Capture()
        {
            return View();
        }

        public async Task<JsonResult> Rate()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders
              .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Response = await client.GetAsync("https://openapi.taifex.com.tw/v1/DailyForeignExchangeRates");
            Response.EnsureSuccessStatusCode();
            string json = await Response.Content.ReadAsStringAsync();
            ExchangeRate[] exArr = JsonSerializer.Deserialize<ExchangeRate[]>(json);
            return Json(exArr.Select(rate => Convert.ToDecimal(rate.USDNTD)).Last());
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
    }
}

using CupSettingsTest.Infrastructure;
using CupSettingsTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CupSettingsTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ToggleSettings _toggleSettings;
        private readonly GdasServiceSettings _gdasServiceSettings;

        public HomeController(IOptions<ToggleSettings> toggleSettings, IOptions<GdasServiceSettings> gdasServiceSettings)
        {
            _toggleSettings = toggleSettings.Value;
            _gdasServiceSettings = gdasServiceSettings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            var value = _toggleSettings.PODupCheckForEmeaApj;
            var uri = _gdasServiceSettings.PODupCheckUri;
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Cups()
        {
            var value = _toggleSettings.PODupCheckForEmeaApj;
            var uri = _gdasServiceSettings.PODupCheckUri;
            ViewData["Message"] = $"PODupCheckForEmeaApj Toggle Value: {value} and PODupCheckUri Value: {uri}";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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

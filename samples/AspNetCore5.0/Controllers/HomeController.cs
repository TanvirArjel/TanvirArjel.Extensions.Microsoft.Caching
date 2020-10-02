using System.Diagnostics;
using System.Threading.Tasks;
using AspNetCore5._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using TanvirArjel.Extensions.Microsoft.Caching;

namespace AspNetCore5._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCache _distributedCache;

        public HomeController(ILogger<HomeController> logger, IDistributedCache distributedCache)
        {
            _logger = logger;
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            string cacheKey = "Emloyee1";
            Employee employee = new Employee()
            {
                Id = 1,
                Name = "Tanvir"
            };

            await _distributedCache.SetAsync<Employee>(cacheKey, employee);

            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            string cacheKey = "Emloyee1";
            Employee employee = await _distributedCache.GetAsync<Employee>(cacheKey);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

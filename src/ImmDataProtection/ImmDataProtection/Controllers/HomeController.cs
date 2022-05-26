using ImmDataProtection.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.DataProtection;

namespace ImmDataProtection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataProtector _dataProtector;

        public HomeController(ILogger<HomeController> logger, IDataProtectionProvider dataProtectionProvider)
        {
            _logger = logger;
            _dataProtector = dataProtectionProvider.CreateProtector("ImmDataProtection.Controllers.HomeController",
                new string[] { "Tenant1" });
        }

        public IActionResult Index()
        {
            var secret1 = _dataProtector.Protect("ST123");
            var secret2 = _dataProtector.Protect("FC345");
            return View(new string[]{secret1,secret2});
        }

        public IActionResult Privacy()
        {
            var inData =
                @"CfDJ8E__A8jkudBPtmoi1ShWBFs9ivxl4dcAyTNAPYh91dtJ4JNE0VAjlHMYiDOrKSqMN59u5UU_92oB4_-oNxkU35kiETf__C_nFP6KANcU_XEFNa3k_zLvlY2yUHIfiWbSXg";
            var myData = _dataProtector.Unprotect(inData);
            return View(model:myData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
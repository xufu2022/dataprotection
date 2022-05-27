using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrmWebDataProtection.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataProtector _dataProtector;
        public IndexModel(ILogger<IndexModel> logger, IDataProtectionProvider dataProtectionProvider)
        {
            _logger = logger;
            _dataProtector = dataProtectionProvider.CreateProtector("CrmWebDataProtection.Pages.IndexModel",
                new string[] { "Tenant1" });
        }

        public void OnGet()
        {
            var secret1 = _dataProtector.Protect("ST123");
            var secret2 = _dataProtector.Protect("FC345");
            ViewData["secret1"] = secret1;
        }
    }
}
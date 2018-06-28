using Microsoft.AspNetCore.Mvc;

namespace AspNetSecurity_m4.Controllers
{
    public class AccountController: Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

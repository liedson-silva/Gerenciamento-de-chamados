using Microsoft.AspNetCore.Mvc;

namespace ProjetoChamados.Controllers
{
    public class UserConfigurationController : Controller
    {
        public IActionResult Index(string user, string password)
        {
            ViewBag.UserName = user;
            ViewBag.UserPassword = password;
            return View();
        }
    }
}

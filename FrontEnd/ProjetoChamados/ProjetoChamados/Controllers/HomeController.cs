using Microsoft.AspNetCore.Mvc;

namespace ProjetoChamados.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string user, string password)
        {
            ViewBag.UserName = user;
            ViewBag.UserPassword = password;
            return View();
        }
    }
}

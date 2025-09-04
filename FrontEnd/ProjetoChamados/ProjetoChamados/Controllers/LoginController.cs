using Microsoft.AspNetCore.Mvc;

namespace ProjetoChamados.Controllers
{
    public class LoginController : Controller
    {
        private readonly List<(string Name, string Password)> users =
        [
            ("Tirulipa", "123"),
            ("Neymar Junior", "123"),
            ("1", "1")
        ];

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name, string password)
        {
            var userFound = users.FirstOrDefault(u => u.Name == name && u.Password == password);

            if (userFound != default)
            {
                return RedirectToAction("Index", "Home", new { user = userFound.Name, password = userFound.Password });
            }
            else
            {
                ViewBag.Error = "Usuário ou senha inválidos!";
                return View();
            }
        }

        [HttpPost]
        public IActionResult ForgetPassword()
        {
            ViewBag.ForgetPassword = "Entre em contato com o administrador para recuperar a senha";
            return View("Index");
        }
    }
}

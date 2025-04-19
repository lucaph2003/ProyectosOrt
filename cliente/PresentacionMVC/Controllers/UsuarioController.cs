using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentacionMVC.Models;

namespace PresentacionMVC.Controllers
{
    public class UsuarioController : Controller
    {
        public IConfiguration Conf { get; set; }
        public string URLBaseApiUsuario { get; set; }

        public UsuarioController(IConfiguration conf)
        {
            Conf = conf;
            URLBaseApiUsuario = Conf.GetValue<string>("ApiUsuario");
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioModel usuario)
        {
            return View();
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Login()
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioModel vm)
        {
            HttpClient client = new HttpClient();

            vm.Nombre = "";
            vm.Rol = "";

            var tarea = client.PostAsJsonAsync(URLBaseApiUsuario, vm);
            tarea.Wait();

            var tarea2 = tarea.Result.Content.ReadAsStringAsync();
            tarea2.Wait();

            string body = tarea2.Result;

            if (tarea.Result.IsSuccessStatusCode)
            {
                LoginDTO info = JsonConvert.DeserializeObject<LoginDTO>(body);

                HttpContext.Session.SetString("token", info.TokenJWT);
                HttpContext.Session.SetString("rol", info.Rol);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = body;
                return View(vm);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

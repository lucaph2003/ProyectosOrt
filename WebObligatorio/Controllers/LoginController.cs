using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace WebObligatorio.Controllers
{
    public class LoginController : Controller
    {
        Sistema sistema = Sistema.ObtenerInstancia;


        public IActionResult Index()
        {
            HttpContext.Session.Remove("UsuarioLogueadoEmail");
            HttpContext.Session.Remove("UsuarioLogueadoNombre");
            HttpContext.Session.Remove("UsuarioRol");
            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario usuario)
        {
            try
            {
                sistema.Login(usuario);
            }
            catch (Exception e)
            {
                ViewBag.NombreError = e.Message;
                return View();
            }

            Usuario usuarioDeSistema = sistema.ObtenerUsuarioPorEmail(usuario.email);

            HttpContext.Session.SetString("UsuarioLogueadoEmail", usuarioDeSistema.email);
            HttpContext.Session.SetString("UsuarioLogueadoNombre", usuarioDeSistema.nombre);
            HttpContext.Session.SetString("UsuarioRol", usuarioDeSistema.ObtenerRol());
            


            return RedirectToAction("Index", "Home");
        }

    }
}

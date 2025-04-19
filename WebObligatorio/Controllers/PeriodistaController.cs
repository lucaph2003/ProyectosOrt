using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace WebObligatorio.Controllers
{
    public class PeriodistaController : Controller
    {
        Sistema sistema = Sistema.ObtenerInstancia;
        public IActionResult ListarPeriodistas()
        {
            string rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != null && rol == ("Operador"))
            {
                List<Periodista> periodistas = sistema.ObtenerPeriodistas();
                if (periodistas.Count < 1)
                {
                    ViewBag.Error = "no hay periodistas registrados";
                }
                return View(periodistas);
            }
            else
            {
                TempData["mensajeError"] = "No tienes permisos para acceder a esta página.";
                return RedirectToAction("MostrarError", "Error");
            }    
        }
    }
}

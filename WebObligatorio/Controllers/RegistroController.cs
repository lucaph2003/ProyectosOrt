using Dominio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebObligatorio.Controllers
{
    public class RegistroController : Controller
    {
        private Sistema sistema = Sistema.ObtenerInstancia;
        public IActionResult Index()
        {
            return View();
        }

       


        [HttpPost]
        public IActionResult Index(Periodista usuario)
        {
            try
            {
                sistema.AltaPeriodista(usuario);
            }catch(Exception e)
            {
                ViewBag.NombreError = e.Message;
                return View();
            }
            
            return RedirectToAction("Index", "Login");
        }

    }
}

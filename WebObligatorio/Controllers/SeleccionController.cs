using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace WebObligatorio.Controllers
{
    public class SeleccionController : Controller
    {
        public Sistema sistema = Sistema.ObtenerInstancia;
        public IActionResult Index()
        {
            List<Seleccion> selecciones = sistema.ObtenerListaSeleccion();
            return View(selecciones);
        }

        public IActionResult Jugadores(int idPais)
        {
            ViewBag.Pais = sistema.GetPaisId(idPais);
            List<Jugador> jugadores = sistema.ObtenerJugadoresPais(idPais);
            return View(jugadores);
        }

        public IActionResult Goleadores()
        {
            string rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != null && rol == ("Operador"))
            {
                ViewBag.Goles = sistema.CantidadGoles();
                List<Seleccion> selecciones = sistema.ObtenerSeleccionGoleadora();
                return View(selecciones);
            }
            else
            {
                TempData["mensajeError"] = "No tienes permisos para acceder a esta página.";
                return RedirectToAction("MostrarError", "Error");
            }
        }
    }
}

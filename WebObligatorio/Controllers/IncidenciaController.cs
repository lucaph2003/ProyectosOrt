using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Dominio;

namespace WebObligatorio.Controllers
{
    public class IncidenciaController : Controller
    {
        Sistema sistema = Sistema.ObtenerInstancia;
        public IActionResult Incidencias(int idPartido)
        {
            string rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != null)
            {
                List<Incidencia> incidencias = sistema.ObtenerListaInciaPorIdPartido(idPartido);
                return View(incidencias);
            }
            else
            {
                TempData["mensajeError"] = "No tienes permisos para acceder a esta página.";
                return RedirectToAction("MostrarError", "Error");
            }
        }

    }
}


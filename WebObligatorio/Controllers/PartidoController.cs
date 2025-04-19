using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Dominio;

namespace WebObligatorio.Controllers
{
    public class PartidoController : Controller
    {
        Sistema sistema = Sistema.ObtenerInstancia;

        //Vista del listado de partidos sin finalizar
        public IActionResult ListadoPartido()
        {
            string rol = HttpContext.Session.GetString("UsuarioRol");
            if(rol != null && rol == ("Operador"))
            {
                List<Partido> partidos = sistema.ObtenerListaPartidos();
                ViewBag.Resultado = "";
                return View(partidos);
            }
            else
            {
                TempData["mensajeError"] = "No tienes permisos para acceder a esta página.";
                return RedirectToAction("MostrarError", "Error");
            }
        }

        //Vista de los partidos finalizados
        public IActionResult ListadosPartidosFinalizados()
        {
            //ViewBag.pe = sistema.ObtenerPartidoFaseEliminatoria();
            string rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != null)
            {
                List<Partido> partidos = sistema.ObtenerListaPartidosFinalizados();
                if(partidos.Count < 1)
                {
                    ViewBag.Error = "No hay partidos finalizados";
                }
                return View(partidos);
            }
            else
            {
                TempData["mensajeError"] = "No tienes permisos para acceder a esta página.";
                return RedirectToAction("MostrarError", "Error");
            }

        }

        //Vista de Finalizar la eliminatoria
        public IActionResult FinalizarEliminatoria(int idPartido)
        {
            string rol = HttpContext.Session.GetString("UsuarioRol");
            if(rol != null && rol == ("Operador"))
            {
                ViewBag.idPartido = idPartido;
                return View();
            }
            else
            {
                TempData["mensajeError"] = "No tienes permisos para acceder a esta página.";
                return RedirectToAction("MostrarError", "Error");
            }
            
        }

        //Vista de Buscar partido por 2 fechas
        public IActionResult BuscarPartidosEntreDosFechas(DateTime f1, DateTime f2)
        {
            string rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != null && rol == ("Operador"))
            {
                if(f1 != null && f2 != null && f1.Year != 0001 && f2.Year != 0001)
                {
                    List<Partido> partidos = sistema.ObtenerPartidosEntre2Fechas(f1, f2);
                    if(partidos.Count < 1)
                    {
                        ViewBag.Error = "No hay partidos entre estas fechas";
                    }
                    return View(partidos);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                TempData["mensajeError"] = "No tienes permisos para acceder a esta página.";
                return RedirectToAction("MostrarError", "Error");
            }

        }

        public IActionResult BuscarPartidoEmailPeriodista(string email)
        {
            string rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != null && rol == ("Operador"))
            {
                if(email != null)
                {
                    List<Partido> partidos = sistema.ObtenerPartidoRojaReseniaEmailPeriodista(email);
                    if (partidos.Count < 1)
                    {
                        ViewBag.Error = "No hay partidos con roja reseniados por "+email;
                    }
                    return View(partidos);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                TempData["mensajeError"] = "No tienes permisos para acceder a esta página.";
                return RedirectToAction("MostrarError", "Error");
            }
        }


        public IActionResult Finalizar(int idPartido)
        {
            try
            {
                sistema.FinalizarPartido(idPartido);
            }
            catch (Exception e)
            {
                ViewBag.ErrorNombre = e.Message;
                return View();
            }
            return RedirectToAction("ListadoPartido", "Partido");
        }


        
        

        






    }
}

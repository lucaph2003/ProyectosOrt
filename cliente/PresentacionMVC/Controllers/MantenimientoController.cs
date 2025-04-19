using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentacionMVC.Models;
using System.Net.Http.Headers;

namespace PresentacionMVC.Controllers
{
    public class MantenimientoController : Controller
    {
        public string URLBaseApiMantenimiento { get; set; }
        public IConfiguration Conf { get; set; }
        public MantenimientoController(IConfiguration conf)
        {
            Conf = conf;
            URLBaseApiMantenimiento = Conf.GetValue<string>("ApiMantenimiento");
        }

        // GET: MantenimientoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MantenimientoController/Create
        public ActionResult Create(int id)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            try
            {
                MantenimientoModel mm = new MantenimientoModel();
                mm.CabaniaId = id;
                return View(mm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(id);
            }
        }

        // POST: MantenimientoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MantenimientoModel MM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = MM;
                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    Task<HttpResponseMessage> tarea = cliente.PostAsJsonAsync(URLBaseApiMantenimiento, MM);
                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListarTodas","Cabania");
                    }
                    else
                    {

                        ViewBag.Error = LeerContenido(respuesta);
                    }
                }
                else
                {
                    ViewBag.Error = "Los datos ingresados no son válidos";
                }

                return View(MM.CabaniaId);

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Oops! Ocurrió un error inesperado";
                return View(MM.CabaniaId);
            }
        }

        //GET
        public ActionResult ListarTodos(int id,DateTime fechaInicio,DateTime fechaFinal)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            try
            {
                    string formatoinicio = fechaInicio.ToString("yyyy-MM-ddTHH:mm:ss");
                    string formatofinal = fechaFinal.ToString("yyyy-MM-ddTHH:mm:ss");
                    HttpClient cliente = new HttpClient();
                    string url = URLBaseApiMantenimiento + "/" + id+"/"+ formatoinicio + "/" + formatofinal;
                    cliente.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    var tarea = cliente.GetAsync(url);
                    tarea.Wait();
                    string cuerpo = LeerContenido(tarea.Result);

                    if (tarea.Result.IsSuccessStatusCode)
                    {
                        IEnumerable<MantenimientoModel> mantenimientos = JsonConvert.DeserializeObject<IEnumerable<MantenimientoModel>>(cuerpo);
                        return View(mantenimientos);
                    }
                    else
                    {
                        throw new Exception(cuerpo);
                    }
                
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        private string LeerContenido(HttpResponseMessage respuesta)
        {
            HttpContent contenido = respuesta.Content;
            Task<string> tarea = contenido.ReadAsStringAsync();
            tarea.Wait();
            return tarea.Result;
        }
    }
}

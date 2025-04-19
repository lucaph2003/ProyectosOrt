using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentacionMVC.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PresentacionMVC.Controllers
{
    public class CabaniaController : Controller
    {
        private IWebHostEnvironment _environment;
        public IConfiguration Conf { get; set; }
        public string URLBaseApiCabania { get; set; }
        public string URLBaseApiTipo { get; set; }
        public CabaniaController( IWebHostEnvironment environment,IConfiguration conf)
        {
            _environment = environment;
            Conf = conf;
            URLBaseApiCabania = Conf.GetValue<string>("ApiCabania");
            URLBaseApiTipo = Conf.GetValue<string>("ApiTipo");
        }

        // GET: CabaniaController1/Create
        public ActionResult Create()
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            AltaCabaniaModel acm = new AltaCabaniaModel();
            try
            {
                IEnumerable<TipoModel> Tipos = ObtenerTipos();
                acm.Tipos = Tipos;
                return View(acm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(acm);
            }
        }

        public List<TipoModel> ObtenerTipos()
        {
            HttpClient client = new HttpClient();
            var tarea = client.GetAsync(URLBaseApiTipo);
            tarea.Wait();
            var tarea2 = tarea.Result.Content.ReadAsStringAsync();
            tarea2.Wait();

            if (tarea.Result.IsSuccessStatusCode)
            {
                List<TipoModel> tipos = JsonConvert.DeserializeObject<List<TipoModel>>(tarea2.Result);
                return tipos;
            }
            else
            {
                return new List<TipoModel>();
            }
        }


        // POST: CabaniaController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AltaCabaniaModel acm, IFormFile Foto)
        {
            CabaniaModel cm = new CabaniaModel();
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = acm;
                    cm = acm.Cabania;
                    if (!GuardarImagen(Foto, cm))
                    {
                        throw new Exception("La imagen no se pudo guardar correctamente");
                    }
                    var obj2 = cm;
                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    Task<HttpResponseMessage> tarea = cliente.PostAsJsonAsync(URLBaseApiCabania, cm);
                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(ListarTodas));
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

                return View();

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Oops! Ocurrió un error inesperado";
                return View();
            }

        }
        private bool GuardarImagen(IFormFile imagen,CabaniaModel cabania)
        {

            if (imagen == null || cabania == null) return false;
            // SUBIR LA IMAGEN
            //ruta física de wwwroot
            string rutaFisicaWwwRoot = _environment.WebRootPath;

            string nombreImagen = imagen.FileName;
            //ruta donde se guardan las fotos de las personas
            string rutaFisicaFoto = Path.Combine
            (rutaFisicaWwwRoot, "img", "fotos", nombreImagen);
            //FileStream permite manejar archivos
            try
            {
                //el método using libera los recursos del objeto FileStream al finalizar
                using (FileStream f = new FileStream(rutaFisicaFoto, FileMode.Create))
                {
                    //Para archivos grandes o varios archivos usar la versión
                    //asincrónica de CopyTo. Sería: await imagen.CopyToAsync (f);
                    imagen.CopyTo(f);
                }
                //GUARDAR EL NOMBRE DE LA IMAGEN SUBIDA EN EL OBJETO
                cabania.Foto = nombreImagen;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //GET: TipoController/Delete/5
        public ActionResult Buscar()
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        //GET: TipoController/Search/6
        public ActionResult BuscarPorNombre()
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult BuscarPorNombre(string Nombre)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            try
            {
                HttpClient cliente = new HttpClient();
                string url = URLBaseApiCabania + "/BuscarNombre/" + Nombre;
                cliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                var tarea = cliente.GetAsync(url);
                tarea.Wait();
                string cuerpo = LeerContenido(tarea.Result);

                if (tarea.Result.IsSuccessStatusCode)
                {
                    IEnumerable<CabaniaModel> cabanias = JsonConvert.DeserializeObject<IEnumerable<CabaniaModel>>(cuerpo);
                    return View(cabanias);
                }
                else
                {
                    throw new Exception(cuerpo);
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        //GET: TipoController/Search/6
        public ActionResult BuscarPorTipo()
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            try
            {
                ViewBag.Tipos = ObtenerTipos();
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public ActionResult BuscarPorTipo(int tipoId)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            try
            {
                if(tipoId == null)
                {
                    ViewBag.Tipos = ObtenerTipos();
                    return View();
                }
                else
                {
                    ViewBag.Tipos = ObtenerTipos();
                    HttpClient cliente = new HttpClient();
                    string url = URLBaseApiCabania + "/BuscarTipo/" + tipoId;
                    cliente.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    var tarea = cliente.GetAsync(url);
                    tarea.Wait();
                    string cuerpo = LeerContenido(tarea.Result);

                    if (tarea.Result.IsSuccessStatusCode)
                    {
                        IEnumerable<CabaniaModel> cabanias = JsonConvert.DeserializeObject<IEnumerable<CabaniaModel>>(cuerpo);
                        return View(cabanias);
                    }
                    else
                    {
                        throw new Exception(cuerpo);
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }
                
        

        //GET: TipoController/Search/6
        public ActionResult BuscarPorCantPersonas()
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public ActionResult BuscarPorCantPersonas(int cantMax,string verificacion)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            try
            {
                HttpClient cliente = new HttpClient();
                string url = URLBaseApiCabania + "/BuscarCantidadMaxima/" + cantMax+"/"+verificacion;
                cliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                var tarea = cliente.GetAsync(url);
                tarea.Wait();
                string cuerpo = LeerContenido(tarea.Result);

                if (tarea.Result.IsSuccessStatusCode)
                {
                    IEnumerable<CabaniaModel> cabanias = JsonConvert.DeserializeObject<IEnumerable<CabaniaModel>>(cuerpo);
                    return View(cabanias);
                }
                else
                {
                    throw new Exception(cuerpo);
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }

        }

        //GET: TipoController/Search/6
        public ActionResult BuscarPorHabilitadas()
        {
            HttpClient client = new HttpClient();
            string url = URLBaseApiCabania + "/BuscarHabilitadas/true";
            var tarea = client.GetAsync(url);
            tarea.Wait();
            var tarea2 = tarea.Result.Content.ReadAsStringAsync();
            tarea2.Wait();

            if (tarea.Result.IsSuccessStatusCode)
            {
                List<CabaniaModel> tipos = JsonConvert.DeserializeObject<List<CabaniaModel>>(tarea2.Result);
                return View(tipos);
            }
            else
            {
                ViewBag.Mensaje = tarea2.Result;
                return View(new List<CabaniaModel>());
            }
        }

        //GET: TipoController/Search/6
        public ActionResult ListarTodas()
        {
            HttpClient cliente = new HttpClient();
            string url = "http://localhost:5072/api/Cabania";
            Task<HttpResponseMessage> respuesta = cliente.GetAsync(url);
            respuesta.Wait();
            HttpContent contenido = respuesta.Result.Content;
            Task<string> tarea2 = contenido.ReadAsStringAsync();
            tarea2.Wait();
            string json = tarea2.Result;
            if (respuesta.Result.IsSuccessStatusCode)
            {
                
                List<CabaniaModel> cabanias = JsonConvert.DeserializeObject<List<CabaniaModel>>(json);
                return View(cabanias);
            }
            else
            {

            }
            return View();
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentacionMVC.Models;
using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PresentacionMVC.Controllers
{
    public class TipoController : Controller
    {
        public string URLBaseApiTipo { get; set; }
        public IConfiguration Conf { get; set; }
        public TipoController(IConfiguration conf)
        {
            Conf = conf;
            URLBaseApiTipo = Conf.GetValue<string>("ApiTipo");
        }

        // GET: TipoController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarTodos()
        {
            HttpClient client = new HttpClient();
            var tarea = client.GetAsync(URLBaseApiTipo);
            tarea.Wait();
            var tarea2 = tarea.Result.Content.ReadAsStringAsync();
            tarea2.Wait();

            if (tarea.Result.IsSuccessStatusCode)
            {
                List<TipoModel> tipos = JsonConvert.DeserializeObject<List<TipoModel>>(tarea2.Result);
                return View(tipos);
            }
            else
            {
                ViewBag.Mensaje = tarea2.Result;
                return View(new List<TipoModel>());
            }
        }

       

        // GET: TipoController/Create
        public ActionResult Create()
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

        // POST: TipoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoModel tm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = tm;
                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    Task<HttpResponseMessage> tarea = cliente.PostAsJsonAsync(URLBaseApiTipo, tm);
                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(ListarTodos));
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

                return View(tm);

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Oops! Ocurrió un error inesperado";
                return View(tm);
            }

        }

        // GET: TipoController/Edit/5
        public ActionResult Edit(string nombre)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            try
            {
                TipoModel vm = BuscarTipo(nombre);
                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        // POST: TipoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoModel tm)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    string url = URLBaseApiTipo +"/"+ tm.Id;
                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    Task<HttpResponseMessage> tarea = cliente.PutAsJsonAsync(url, tm);
                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(ListarTodos));
                    }
                    else
                    {
                        if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                            respuesta.StatusCode == System.Net.HttpStatusCode.Forbidden)
                        {
                            return RedirectToAction("Login","Usuario");
                        }
                        else
                        {
                            ViewBag.Error = LeerContenido(respuesta);
                        }
                    }
                }
                else
                {
                    ViewBag.Error = "Los datos ingresados no son válidos";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(tm);
            }

            return View(tm);
        }

        //GET: TipoController/Delete/5
        public ActionResult Delete(string nombre)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            try
            {
                TipoModel vm = BuscarTipo(nombre);
                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }


        //POST: TipoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                HttpClient cliente = new HttpClient();
                string url = URLBaseApiTipo+"/" + id;
                cliente.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                var tarea = cliente.DeleteAsync(url);
                tarea.Wait();

                HttpResponseMessage respuesta = tarea.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(ListarTodos));
                }
                else
                {
                    ViewBag.Error = LeerContenido(respuesta);
                    return View();
                }
            }
            catch
            {
                ViewBag.Mensaje = "No se pudo eliminar el tema";
                return View();
            }
        }

        //GET: TipoController/Search/6
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
        //GET
        [HttpGet]
        public ActionResult Buscar(string nombre)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == null || rol != "Funcionario") return RedirectToAction("Login", "Usuario");
            try
            {
                TipoModel vm = BuscarTipo(nombre);
                return View(vm);
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

        private TipoModel BuscarTipo(string nombre)
        {
            HttpClient cliente = new HttpClient();
            string url = URLBaseApiTipo +"/" +nombre;
            cliente.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var tarea = cliente.GetAsync(url);
            tarea.Wait();
            string cuerpo = LeerContenido(tarea.Result);

            if (tarea.Result.IsSuccessStatusCode)
            {
                TipoModel tipo = JsonConvert.DeserializeObject<TipoModel>(cuerpo);
                return tipo;
            }
            else
            {
                throw new Exception(cuerpo);
            }
        }

    }
}

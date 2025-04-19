using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebObligatorio.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult MostrarError()
        {
            return View();
        }
    }
}

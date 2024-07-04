using Aplicacion.CUCabania;
using Aplicacion.CUMantenimiento;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {
        public IAltaMantenimiento AltaMantenimiento { get; set; }
        public IListadoMantenimientos ListadoMantenimientos { get; set; }
        public IBuscarCabania BuscarCabania { get; set; }
        public MantenimientoController(IAltaMantenimiento altaMantenimiento, IListadoMantenimientos listadoMantenimientos, IBuscarCabania buscarCabania)
        {
            AltaMantenimiento = altaMantenimiento;
            ListadoMantenimientos = listadoMantenimientos;
            BuscarCabania = buscarCabania;
        }

        // GET api/<MantenimientoController>/5
        [HttpGet("{idCabania}/{fecha1}/{fecha2}")]
        public IActionResult Get(int idCabania, string fecha1, string fecha2)
        {
            try
            {
                DateTime fechaInicioDateTime = DateTime.Parse(fecha1);
                DateTime fechaFinDateTime = DateTime.Parse(fecha2);
                CabaniaDTO cabania = BuscarCabania.FindById(idCabania);
                IEnumerable<MantenimientoDTO> mantenimientos = ListadoMantenimientos.ListarMantenimientosEntreDosFechas(cabania, fechaInicioDateTime, fechaFinDateTime);
                return Ok(mantenimientos);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<MantenimientoController>
        [HttpPost]
        public IActionResult Post([FromBody] MantenimientoDTO M)
        {
            try
            {
                AltaMantenimiento.Alta(M);
                return Ok();
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

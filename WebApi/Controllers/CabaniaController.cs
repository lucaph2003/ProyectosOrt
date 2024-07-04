using Aplicacion.CUCabania;
using Aplicacion.CUTipo;
using Microsoft.AspNetCore.Mvc;
using DTOs;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabaniaController : ControllerBase
    {
        public IAltaCabania AltaCabania { get; set; }
        public IBuscarCabania BuscarCabania { get; set; }
        public IListadoCabanias ListadoCabanias { get; set; }
        public IListadoTipos ListadoTipos { get; set; }
        public IBuscarTipo BuscarTipo { get; set; }
        public CabaniaController(IAltaCabania altaCabania, IBuscarCabania buscarCabania, IListadoCabanias listadoCabanias, IListadoTipos listadoTipos, IBuscarTipo buscarTipo)
        {
            AltaCabania = altaCabania;
            BuscarCabania = buscarCabania;
            ListadoCabanias = listadoCabanias;
            ListadoTipos = listadoTipos;
            BuscarTipo = buscarTipo;
        }


        // GET: api/<CabaniaController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(ListadoCabanias.FindAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<CabaniaController>/5
        [HttpGet("BuscarNombre/{nombre}")]
        public IActionResult Get(string? nombre)
        {
            try
            {
                IEnumerable<CabaniaDTO> cabanias = BuscarCabania.BuscarPorAtributo(nombre, null, null, null);
                return Ok(cabanias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<CabaniaController>/5
        [HttpGet("BuscarTipo/{tipoid}")]
        public IActionResult Get(int? tipoid)
        {
            try
            {
                IEnumerable<CabaniaDTO> cabanias = BuscarCabania.BuscarPorAtributo(null, tipoid, null, null);
                return Ok(cabanias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<CabaniaController>/5
        [HttpGet("BuscarCantidadMaxima/{cantidadMaxima}/{verificacion}")]
        public IActionResult Get(int? cantidadMaxima,string verificacion)
        {
            try
            {
                verificacion = null;
                IEnumerable<CabaniaDTO> cabanias = BuscarCabania.BuscarPorAtributo(null, null, cantidadMaxima, null);
                return Ok(cabanias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<CabaniaController>/5
        [HttpGet("BuscarHabilitadas/{Habilitadas}")]
        public IActionResult Get(bool? Habilitadas)
        {
            try
            {
                IEnumerable<CabaniaDTO> cabanias = BuscarCabania.BuscarPorAtributo(null, null, null, Habilitadas);
                return Ok(cabanias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // POST api/<CabaniaController>
        [HttpPost]
        public IActionResult Post([FromBody] CabaniaDTO c)
        {
            try
            {
                AltaCabania.Alta(c);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

using Aplicacion.CUTipo;
using DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoController : ControllerBase
    {
        public IAltaTipo AltaTipo { get; set; }
        public IBuscarTipo BuscarTipo { get; set; }
        public IEliminarTipo EliminarTipo { get; set; }
        public IListadoTipos ListadoTipos { get; set; }
        public IModificarTipo ModificarTipo { get; set; }
        public TipoController(IAltaTipo altaTipo, IBuscarTipo buscarTipo, IEliminarTipo eliminarTipo, IListadoTipos listadoTipos, IModificarTipo modificarTipo)
        {
            AltaTipo = altaTipo;
            BuscarTipo = buscarTipo;
            EliminarTipo = eliminarTipo;
            ListadoTipos = listadoTipos;
            ModificarTipo = modificarTipo;
        }
        // GET: api/<TemaController>
        [HttpGet(Name = "findAll")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<TipoDTO> tipos = ListadoTipos.ObtenerListado();
                return Ok(tipos);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<TemaController>/5
        [HttpGet("{nombre}")]
        public IActionResult Get(string nombre)
        {
            try
            {
                TipoDTO tipo = BuscarTipo.BuscarPorNombre(nombre);
                return Ok(tipo);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<TemaController>
        [HttpPost]
        public IActionResult Post([FromBody] TipoDTO? t)
        {
            try
            {
                AltaTipo.Alta(t);
                return Ok();
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<TemaController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TipoDTO? t)
        {
            try
            {
                ModificarTipo.Modificar(t);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<TemaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                EliminarTipo.Eliminar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
    }
}

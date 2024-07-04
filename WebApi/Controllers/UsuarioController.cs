using Aplicacion.CUUsuario;
using DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public ILoginUsuario LoginUsuario { get; set; }
        public IAltaUsuario AltaUsuario { get; set; }
        public UsuarioController(ILoginUsuario LoginUsuario, IAltaUsuario altaUsuario)
        {
            this.LoginUsuario = LoginUsuario;
            AltaUsuario = altaUsuario;
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioDTO? u)
        {
            try
            {
                UsuarioDTO usuario = LoginUsuario.Login(u);
                return Ok(new LoginDTO() { Rol = usuario.Rol, TokenJWT = ManejadorJWT.GenerarToken(usuario) });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

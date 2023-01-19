using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System.Net;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuariosRepositorio repository = new UsuariosRepositorio();
        //TRAE TODO LOS USUARIOS
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Usuario> lista = repository.ListarUsuario();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet("{nombreUsuario}")]

        //TRAE USUARIO POR NOMBREUSUARIO
        public ActionResult<Usuario> Get(string nombreUsuario)
        {
            try
            {
                Usuario? usuario = repository.getUsuarioNombreUsuario(nombreUsuario);
               if(usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound("El usuario no existe");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{nombreUsuario}/{contrasenia}")]
        // INICIO DE SESION
        public ActionResult<Usuario> Get(string nombreUsuario, string contrasenia)
        {
            try
            {
                Usuario? usuario = repository.getUsuarioNombreUsuario(nombreUsuario);
                if (usuario != null)
                {
                    bool logeado = repository.login(nombreUsuario, contrasenia);
                    if (logeado)
                    {
                        return Ok(usuario);
                    }
                    else
                    {
                        return Unauthorized("Contraseña invalida.");
                    }
                }
                else
                {
                    return NotFound("El usuario no existe.");
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpDelete]

        //SE BORRA EL USUARIO POR ID
        public ActionResult Delete([FromBody] int id)
        {
            try
            {
                bool seElimino = repository.eliminarUsuario(id);
                if (seElimino)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                repository.CrearUsuario(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult<Usuario> Put(int id,[FromBody] Usuario usuarioAeditar)
        {
            try
            {
                Usuario? usuario = repository.editarUsuario(id, usuarioAeditar);
                if(usuario != null)
                {
                    return Ok();
                }
                else
                {
                    return NotFound($"El ususario con id {id} no existe!!");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System.Net;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private ProductosRepositorio repository = new ProductosRepositorio();

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Producto> lista = repository.ListarProductos();
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }
        [HttpGet("{id}")]
        // TRAER PRODUCTOS POR ID
        public ActionResult<Producto> Get(int id)
        {
            try
            {
                List<Producto> productos = repository.getProductoPorId(id);
                if (productos != null)
                {
                    return Ok(productos);
                }
                else
                {
                    return NotFound("Product Not Found.");
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                bool seElimino = repository.eliminarProducto(id);
                if (seElimino)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
          
        }

        [HttpPost]
        public ActionResult Post([FromBody] Producto producto)
        {
            try
            {
                repository.CrearProducto(producto);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Producto productoAeditar)
        {
            try
            {
                Producto producto = repository.editarProducto(id, productoAeditar);
                if (producto != null)
                {
                    return Ok(producto);
                }
                else
                {
                    return NotFound($"El Producto con id {id}  no existe.");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

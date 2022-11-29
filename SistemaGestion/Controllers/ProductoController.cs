using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System.Net;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}

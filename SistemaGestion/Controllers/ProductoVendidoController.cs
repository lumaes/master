using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System.Net;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : Controller
    {
        private ProductoVendidoRepositorio repository = new ProductoVendidoRepositorio();


        // GET: VentaController
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                List<ProductoVendido> lista = repository.ListarProductoVendido();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System.Net;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : Controller
    {


        private VentaRepositorio repository = new VentaRepositorio();

        
        // GET: VentaController
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                List<Venta> lista = repository.ListarVenta();
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
           
        }

       
    }
}

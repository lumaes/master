using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System.Net;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : Controller
    {


        private VentaRepositorio repository = new VentaRepositorio();

        
        // TRAEMOS LA LISTA DE VENTAS
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                List<Venta> lista = repository.ListarVenta(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
           
        }
        // TRAE EL PRODUCTO POR ID
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                List<Venta> lista = repository.ListarVenta(id);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
        //ELIMINA EL PRODUCTO POR ID
        [HttpDelete]
        public ActionResult Delete([FromBody] int id)
        {
            try
            {
                bool seElimino = repository.eliminarVenta(id);
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
        // CREA UNA VENTA
        [HttpPost]
        public ActionResult Post([FromBody] Venta venta)
        {
            try
            {
                repository.CrearVenta(venta);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // MODIFICAR VENTA
        [HttpPut]
        public ActionResult Put([FromBody] Venta venta)
        {
            try
            {
                repository.editarVenta(venta);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
// PRESENTA EL NOMBRE DE LA APLICAION EN EL FRONTEND
namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NombreController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Sistema De Gestion de ventas-Lucas Escalante";
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParcialApp.Servicios;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        //Agregar referencia del proyecto. Agregar>Referencia del Proyecto
        private IServicio servicio;
    }
    
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BibliotecaClases.Dominio;
using BibliotecaClases.Servicios;
using BibliotecaClases.Servicios.impl;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        //Agregar referencia del proyecto. Agregar>Referencia del Proyecto
        private IServicio servicio;
        private Turno nuevoTurno;

        public TurnoController()
        {
            servicio = new Servicio();
            nuevoTurno = new Turno();
        }

        [HttpGet("GET/medicos")]
        public IActionResult GetAllMedicos()
        {
            try
            {
                var medicos = servicio.ListarMedicos();

                if (medicos == null || !medicos.Any())
                {
                    return NotFound("No se encontraron Medicos");
                }
                return Ok(medicos);
            }
            catch (Exception)
            {
                return BadRequest("No se pueden obtener los medicos");
            }
        }

        //Formato de la fecha en swagger: 2024-11-26  43524884
        [HttpGet("GET/turno/{fecha}/{hora}/{matricula}")]

        public IActionResult GetTurnos(string fecha,string hora,int matricula)
        {
            try
            {
                int cantTurnos = servicio.ListarCantTurnos(Convert.ToDateTime(fecha), Convert.ToDateTime(hora), matricula);

                return Ok(cantTurnos);
            }
            catch (Exception ex)
            {

                return StatusCode(400, "No hay registros");
            }
        }

        [HttpPost("POST/turno/")]

        public IActionResult PostTurno([FromBody] Turno nuevoTurno)
        {
            if(nuevoTurno == null)
            {
                return BadRequest("El turno es nulo");
            }

            if(nuevoTurno.ListaDetalles == null || !nuevoTurno.ListaDetalles.Any())
            {
                return BadRequest("El presupuesto debe tener al menos un detalle.");
            }

            try
            {
                bool resultado = servicio.Crearturno(nuevoTurno);

                if (resultado)
                {
                    return Ok("Presupuesto creado con exito.");
                }
                else
                {
                    return StatusCode(500, "Error interno servidor.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
    
}

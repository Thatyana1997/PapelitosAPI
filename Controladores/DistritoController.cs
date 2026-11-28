using Microsoft.AspNetCore.Mvc;
using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;

namespace PapelitosAPI.Controladores
{
    [Route("api/[controller]")]
    [ApiController]

    public class DistritoController : ControllerBase
    {
        private readonly DistritoRepository _repository;

        public DistritoController(DistritoRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CrearDistrito([FromBody] Distrito distrito)
        {
            await _repository.CrearDistrito(distrito);
            // Nota: Como el ID es autoincremental en BD, el objeto devuelto 
            // podría no tener el ID actualizado a menos que el SP lo retorne.
            return Ok(distrito);
        }

        [HttpGet]
        public async Task<ActionResult<List<Distrito>>> LeerDistrito()
        {
            return await _repository.LeerDistrito();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Distrito>> BuscarDistritoPorId(int id)
        {
            var distrito = await _repository.BuscarDistritoPorId(id);
            if (distrito == null)
            {
                return NotFound();
            }
            return distrito;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarDistrito(int id, [FromBody] Distrito distrito)
        {
            if (id != distrito.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            await _repository.ModificarDistrito(distrito);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDistrito(int id, [FromQuery] string usuarioModificacion)
        {
            // Se requiere el usuario que realiza la eliminación para la auditoría
            if (string.IsNullOrEmpty(usuarioModificacion))
            {
                return BadRequest("El usuario de modificación es requerido para la eliminación.");
            }

            await _repository.EliminarDistrito(id, usuarioModificacion);
            return NoContent();
        }
    }
}

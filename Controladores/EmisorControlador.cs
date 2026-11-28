using Microsoft.AspNetCore.Mvc;
using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;

namespace PapelitosAPI.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmisorControlador : ControllerBase
    {
        private readonly EmisorRepositorio _repository;

        public EmisorControlador(EmisorRepositorio repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CrearEmisor([FromBody] Emisor emisor)
        {
            await _repository.CrearEmisor(emisor);
            // Nota: Como el ID es autoincremental en BD, el objeto devuelto 
            // podría no tener el ID actualizado a menos que el SP lo retorne.
            return Ok(emisor);
        }

        [HttpGet]
        public async Task<ActionResult<List<Emisor>>> LeerEmisor()
        {
            return await _repository.LeerEmisor();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Emisor>> BuscarEmisorPorId(int id)
        {
            var emisor = await _repository.BuscarEmisorPorId(id);
            if (emisor == null)
            {
                return NotFound();
            }
            return emisor;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarEmisor(int id, [FromBody] Emisor emisor)
        {
            if (id != emisor.id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            await _repository.ModificarEmisor(emisor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEmisor(int id, [FromQuery] string usuarioModificacion)
        {
            // Se requiere el usuario que realiza la eliminación para la auditoría
            if (string.IsNullOrEmpty(usuarioModificacion))
            {
                return BadRequest("El usuario de modificación es requerido para la eliminación.");
            }

            await _repository.EliminarEmisor(id, usuarioModificacion);
            return NoContent();
        }
    }
}

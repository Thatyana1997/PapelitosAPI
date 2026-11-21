using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;

namespace PapelitosAPI.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonosController : ControllerBase
    {
        private readonly TelefonoRepositorio _repository;

        public TelefonosController(TelefonoRepositorio repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTelefono([FromBody] Telefonos telefono)
        {
            await _repository.CrearTelefono(telefono);
            // Nota: Como el ID es autoincremental en BD, el objeto devuelto 
            // podría no tener el ID actualizado a menos que el SP lo retorne.
            return Ok(telefono);
        }

        [HttpGet]
        public async Task<ActionResult<List<Telefonos>>> LeerTelefono()
        {
            return await _repository.LeerTelefono();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarTelefono(int id, [FromBody] Telefonos telefono)
        {
            if (id != telefono.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            await _repository.ModificarTelefono(telefono);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTelefono(int id)
        {
            await _repository.EliminarTelefono(id, usuarioModificacion);
            return NoContent();
        }
    }
}

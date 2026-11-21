using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CrearTelefono([FromBody] Telefono telefono)
        {
            await _repository.CrearTelefono(categoria);
            // Nota: Como el ID es autoincremental en BD, el objeto devuelto 
            // podría no tener el ID actualizado a menos que el SP lo retorne.
            return Ok(telefono);
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> LeerCategorias()
        {
            return await _repository.LeerCategorias();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> BuscarCategoriaPorId(int id)
        {
            var categoria = await _repository.BuscarCategoriaPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return categoria;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarCategoria(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            await _repository.ModificarCategoria(categoria);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id, [FromQuery] string usuarioModificacion)
        {
            // Se requiere el usuario que realiza la eliminación para la auditoría
            if (string.IsNullOrEmpty(usuarioModificacion))
            {
                return BadRequest("El usuario de modificación es requerido para la eliminación.");
            }

            await _repository.EliminarCategoria(id, usuarioModificacion);
            return NoContent();
        }
    }
}

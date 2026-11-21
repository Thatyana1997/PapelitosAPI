using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;

namespace PapelitosAPI.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class CantonController : ControllerBase
    {
        private readonly CantonRepository _repository;

        public CantonController(CantonRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCanton([FromBody] Canton canton)
        {
            if (string.IsNullOrWhiteSpace(canton.Usuario_Creacion))
                return BadRequest("El usuario de creación es requerido.");

            try
            {
                var creado = await _repository.CrearCanton(canton);
                return CreatedAtAction(nameof(BuscarCantonPorId), new { id = creado.Id }, creado);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return BadRequest("La provincia indicada no existe.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Canton>>> LeerCantones()
        {
            return await _repository.LeerCantones();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Canton>> BuscarCantonPorId(int id)
        {
            var canton = await _repository.BuscarCantonPorId(id);
            if (canton == null)
                return NotFound();
            return canton;
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> ModificarCanton(int id, [FromBody] Canton canton)
        {
            if (id != canton.Id)
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            if (string.IsNullOrWhiteSpace(canton.Usuario_Modificacion))
                return BadRequest("El usuario de modificación es requerido.");

            try
            {
                if (!await _repository.ModificarCanton(canton))
                    return NotFound();
                return NoContent();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return BadRequest("La provincia indicada no existe.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarCanton(int id, [FromQuery] string usuarioModificacion)
        {
            if (string.IsNullOrWhiteSpace(usuarioModificacion) || usuarioModificacion.Length > 50)
                return BadRequest("El usuario de modificación es requerido y admite hasta 50 caracteres.");
            if (!await _repository.EliminarCanton(id, usuarioModificacion))
                return NotFound();
            return NoContent();
        }
    }
}

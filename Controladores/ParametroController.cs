using Microsoft.AspNetCore.Mvc;
using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;

namespace PapelitosAPI.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametroController : ControllerBase
    {
        private readonly ParametroRepository _repository;

        public ParametroController(ParametroRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CrearParametro([FromBody] Parametro parametro)
        {
            await _repository.CrearParametro(parametro);
            return Ok(parametro);
        }

        [HttpGet]
        public async Task<ActionResult<List<Parametro>>> LeerParametros()
        {
            return await _repository.LeerParametros();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Parametro>> BuscarParametroPorId(int id)
        {
            var parametro = await _repository.BuscarParametroPorId(id);
            if (parametro == null)
            {
                return NotFound();
            }
            return parametro;
        }

        [HttpGet("buscar/{clave}")]
        public async Task<ActionResult<List<Parametro>>> BuscarParametroPorClave(string clave)
        {
            return await _repository.BuscarParametroPorClave(clave);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarParametro(int id, [FromBody] Parametro parametro)
        {
            if (id != parametro.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            await _repository.ModificarParametro(parametro);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarParametro(int id, [FromQuery] string usuarioModificacion)
        {
            if (string.IsNullOrEmpty(usuarioModificacion))
            {
                return BadRequest("El usuario de modificación es requerido para la eliminación.");
            }

            await _repository.EliminarParametro(id, usuarioModificacion);
            return NoContent();
        }
    }
}

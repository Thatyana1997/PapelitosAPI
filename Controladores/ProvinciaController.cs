using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;

namespace PapelitosAPI.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly ProvinciaRepository _repository;

        public ProvinciaController(ProvinciaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CrearProvincia([FromBody] Provincia provincia)
        {
            await _repository.CrearProvincia(provincia);
            return Ok(provincia);
        }

        [HttpGet]
        public async Task<IActionResult> LeerProvincias()
        {
            var provincias = await _repository.LeerProvincias();
            return Ok(provincias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarProvinciaPorId(int id)
        {
            var provincia = await _repository.BuscarProvinciaPorId(id);
            if (provincia == null)
            {
                return NotFound();
            }
            return Ok(provincia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarProvincia(int id, [FromBody] Provincia provincia)
        {
            if (id != provincia.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            await _repository.ModificarProvincia(provincia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProvincia(int id, [FromQuery] string usuarioModificacion)
        {
            if (string.IsNullOrEmpty(usuarioModificacion))
            {
                return BadRequest("El usuario de modificación es requerido para la eliminación.");
            }

            await _repository.EliminarProvincia(id, usuarioModificacion);
            return NoContent();
        }
    }
}

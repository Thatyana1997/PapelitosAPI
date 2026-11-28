using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace PapelitosAPI.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormasPagoController : ControllerBase
    {
        private readonly FormasPagoRepository _repository;

        public FormasPagoController(FormasPagoRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CrearFormasPago([FromBody] FormasPago formasPago)
        {
            await _repository.CrearFormasPago(formasPago);
            return Ok(formasPago);
        }

        [HttpGet]
        public async Task<ActionResult<List<FormasPago>>> LeerFormasPago()
        {
            return await _repository.LeerFormasPago();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormasPago>> BuscarFormaPagoPorId(int id)
        {
            var formaPago = await _repository.BuscarFormaPagoPorId(id);
            if (formaPago == null)
            {
                return NotFound();
            }
            return formaPago;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarFormaPago(int id, [FromBody] FormasPago formaPago)
        {
            if (id != formaPago.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            await _repository.ModificarFormaPago(formaPago);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarFormaPago(int id, [FromQuery] string usuarioModificacion)
        {
            if (string.IsNullOrEmpty(usuarioModificacion))
            {
                return BadRequest("El usuario de modificación es requerido para la eliminación.");
            }

            await _repository.EliminarFormaPago(id, usuarioModificacion);
            return NoContent();
        }
    }
}

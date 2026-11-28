
using Microsoft.AspNetCore.Mvc;
using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;

namespace PapelitosAPI.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleFacturaController : ControllerBase
    {
        private readonly DetalleFacturaRepository _repository;

        public DetalleFacturaController(DetalleFacturaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CrearDetalleFactura([FromBody] DetalleFactura detalle)
        {
            await _repository.CrearDetalleFactura(detalle);
            return Ok(detalle);
        }

        [HttpGet]
        public async Task<ActionResult<List<DetalleFactura>>> LeerDetallesFactura()
        {
            return await _repository.LeerDetallesFactura();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleFactura>> BuscarDetalleFacturaPorId(int id)
        {
            var detalle = await _repository.BuscarDetalleFacturaPorId(id);

            if (detalle == null)
            {
                return NotFound();
            }

            return detalle;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarDetalleFactura(int id, [FromBody] DetalleFactura detalle)
        {
            if (id != detalle.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            await _repository.ModificarDetalleFactura(detalle);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDetalleFactura(int id, [FromQuery] string usuarioModificacion)
        {
            if (string.IsNullOrEmpty(usuarioModificacion))
            {
                return BadRequest("El usuario de modificación es requerido.");
            }

            await _repository.EliminarDetalleFactura(id, usuarioModificacion);
            return NoContent();
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;

namespace PapelitosAPI.Controladores
{
    [Route("api/Factura")]
    [ApiController]
    public class FacturaControlador : ControllerBase
    {
        private readonly FacturaRepositorio _repositorio;

        public FacturaControlador(FacturaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpPost]
        public async Task<IActionResult> CrearFactura([FromBody] Factura factura)
        {
            if (string.IsNullOrEmpty(factura.Usuario_Creacion))
            {
                return BadRequest("El usuario de creación es requerido.");
            }

            factura.Id = await _repositorio.CrearFactura(factura);
            return CreatedAtAction(nameof(BuscarFacturaPorId), new { id = factura.Id }, factura);
        }

        [HttpGet]
        public async Task<ActionResult<List<Factura>>> LeerFacturas()
        {
            return await _repositorio.LeerFacturas();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> BuscarFacturaPorId(int id)
        {
            var factura = await _repositorio.BuscarFacturaPorId(id);
            if (factura == null)
            {
                return NotFound();
            }
            return factura;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarFactura(int id, [FromBody] Factura factura)
        {
            if (id != factura.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            if (string.IsNullOrEmpty(factura.Usuario_Modificacion))
            {
                return BadRequest("El usuario de modificación es requerido.");
            }

            var filas = await _repositorio.ModificarFactura(factura);
            if (filas == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarFactura(int id, [FromQuery] string usuarioModificacion)
        {
            if (string.IsNullOrEmpty(usuarioModificacion))
            {
                return BadRequest("El usuario de modificación es requerido para la eliminación.");
            }

            var filas = await _repositorio.EliminarFactura(id, usuarioModificacion);
            if (filas == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

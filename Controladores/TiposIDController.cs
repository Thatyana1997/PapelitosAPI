using Microsoft.AspNetCore.Mvc;
using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;


namespace PapelitosAPI.Controladores
{

    [Route("api/[controller]")]
    [ApiController]

    public class TiposIDController : ControllerBase

    {
        private readonly TiposIDRepository _repository;

        public TiposIDController(TiposIDRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]

        public async Task<IActionResult> CrearTipoID([FromBody] TipoID tipoID)
        {
            await _repository.CrearTipoID(tipoID);
            return Ok(tipoID);
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoID>>> LeerTiposID()
        {
            return await _repository.LeerTiposID();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoID>> BuscarTipoIDPorId(int id)
        {
            var tipoID = await _repository.BuscarTipoIDPorId(id);
            if (tipoID == null)
            {
                return NotFound();
            }
            return tipoID;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarTipoID(int id, [FromBody] TipoID tipoID)
        {
            if (id != tipoID.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }
            await _repository.ModificarTipoID(tipoID);
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> EliminarTipoID(int id, [FromQuery] string usuarioModificacion)
        {
            if (string.IsNullOrEmpty(usuarioModificacion))
            {
                return BadRequest("Se requiere el usuario que realiza la eliminación para la auditoría.");
            }
            await _repository.EliminarTipoID(id, usuarioModificacion);
            return NoContent();
        }




    }
}

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

        public async Task<IActionResult> CrearTiposID([FromBody] TiposID tipoID)
        {
            await _repository.CrearTipoID(tipoID); //ajuste de la profe
            return Ok(tipoID);
        }

        [HttpGet]
        public async Task<ActionResult<List<TiposID>>> LeerTiposID()
        {
            return await _repository.LeerTiposID();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TiposID>> BuscarTipoIDPorId(int id)
        {
            var tipoID = await _repository.BuscarTipoIDPorId(id); //ajuste de la profe
            if (tipoID == null)
            {
                return NotFound();
            }
            return tipoID;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarTiposID(int id, [FromBody] TiposID tipoID) //ajuste de la profe
        {
            if (id != tipoID.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }
            await _repository.ModificarTipoID(tipoID); //ajuste de la profe
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> EliminarTiposID(int id, [FromQuery] string usuarioModificacion)
        {
            if (string.IsNullOrEmpty(usuarioModificacion))
            {
                return BadRequest("Se requiere el usuario que realiza la eliminación para la auditoría.");
            }
            await _repository.EliminarTipoID(id, usuarioModificacion); //ajuste de la profe
            return NoContent();
        }




    }
}

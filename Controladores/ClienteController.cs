using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;

namespace PapelitosAPI.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepository _repository;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ClienteRepository repository, ILogger<ClienteController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> CrearCliente([FromBody] Cliente cliente)
        {
            if (cliente.Id != 0)
                return BadRequest("El id se genera automaticamente; debe ser 0 al crear.");
            if (string.IsNullOrWhiteSpace(cliente.Usuario_Creacion))
                return BadRequest("El usuario de creacion es obligatorio.");
            if (cliente.Estado != "A" && cliente.Estado != "I")
                return BadRequest("El estado debe ser A o I. Para eliminar utilice DELETE.");
            try
            {
                var creado = await _repository.CrearCliente(cliente);
                return CreatedAtAction(nameof(BuscarClientePorId), new { id = creado.Id }, creado);
            }
            catch (SqlException ex) { return ErrorSql(ex); }
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> LeerClientes()
        {
            try { return await _repository.LeerClientes(); }
            catch (SqlException ex) { return ErrorSql(ex); }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>> BuscarClientePorId(int id)
        {
            if (id <= 0) return BadRequest("El id debe ser positivo.");
            try
            {
                var cliente = await _repository.BuscarClientePorId(id);
                if (cliente == null) return NotFound("El cliente no existe o esta eliminado.");
                return cliente;
            }
            catch (SqlException ex) { return ErrorSql(ex); }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> ModificarCliente(int id, [FromBody] Cliente cliente)
        {
            if (id <= 0 || id != cliente.Id)
                return BadRequest("El id debe ser positivo y coincidir con el cuerpo.");
            if (string.IsNullOrWhiteSpace(cliente.Usuario_Modificacion))
                return BadRequest("El usuario de modificacion es obligatorio.");
            if (cliente.Estado != "A" && cliente.Estado != "I")
                return BadRequest("El estado debe ser A o I. Para eliminar utilice DELETE.");
            try
            {
                if (!await _repository.ModificarCliente(cliente))
                    return NotFound("El cliente no existe o esta eliminado.");
                return NoContent();
            }
            catch (SqlException ex) { return ErrorSql(ex); }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarCliente(int id,
            [FromQuery, Required, StringLength(50)] string usuarioModificacion)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(usuarioModificacion))
                return BadRequest("El id debe ser positivo y el usuario de modificacion obligatorio.");
            try
            {
                if (!await _repository.EliminarCliente(id, usuarioModificacion))
                    return NotFound("El cliente no existe o esta eliminado.");
                return NoContent();
            }
            catch (SqlException ex) { return ErrorSql(ex); }
        }

        private ObjectResult ErrorSql(SqlException ex)
        {
            _logger.LogError(ex, "Error en una operacion de Cliente.");
            if (ex.Number == 547)
                return Problem(statusCode: 400, title: "Tipo de identificacion no valido.",
                    detail: "Id_Tipo_Id debe corresponder a un registro existente en TiposID, o ser null.");
            if (ex.Number == 50001)
                return Problem(statusCode: 400, title: "Datos de Cliente no validos.", detail: ex.Message);
            return Problem(statusCode: 500, title: "No se pudo completar la operacion de Cliente.",
                detail: "Revise la conexion y los procedimientos almacenados. Consulte el registro del servidor.");
        }
    }
}

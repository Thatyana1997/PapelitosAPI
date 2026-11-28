using Microsoft.AspNetCore.Mvc;
using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;

namespace PapelitosAPI.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoRepository _repository;
        public ProductoController(ProductoRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            await _repository.CrearProducto(producto);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> LeerProductos()
        {
            var productos = await _repository.LeerProductos();
            return Ok(productos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarProductoPorId(int id)
        {
            var producto = await _repository.BuscarProductoPorId(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarProducto(int id, [FromBody] Producto producto)
        {
            producto.Id = id;
            await _repository.ModificarProducto(producto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            await _repository.EliminarProducto(id, User.Identity.Name);
            return Ok();
        }
    }
}

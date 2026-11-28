using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;


namespace PapelitosAPI.Repositorios

{
    public class ProductoRepository
    {
        private readonly AppDbContext _context;
        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CrearProducto(Producto producto)
        {
            var parameters = new[]
            {
                new SqlParameter("@nombre", producto.nombre),
                new SqlParameter("@precio", producto.precio),
                new SqlParameter("@id_categoria", producto.id_categoria),
                new SqlParameter("@usuario_creacion", producto.Usuario_Creacion)
            };
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_Productos_Insertar @nombre, @precio, @id_categoria, @usuario_creacion",
                    parameters);
            }
            catch (Exception ex)
            {
                // Considera usar un logger en lugar de Console.WriteLine en producción
                throw new Exception("Error al insertar el producto", ex);
            }
        }
        public async Task<List<Producto>> LeerProductos()
        {
            // Usamos el procedimiento sp_Productos_Consultar sin parámetros para listar todos
            return await _context.Producto.FromSqlRaw("EXEC sp_Productos_Consultar").ToListAsync();
        }
        public async Task<Producto> BuscarProductoPorId(int id)
        {
            var parameter = new SqlParameter("@id", id);
            var result = await _context.Producto
                .FromSqlRaw("EXEC sp_Productos_Consultar @id", parameter)
                .ToListAsync();
            return result.FirstOrDefault();
        }
        public async Task ModificarProducto(Producto producto)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", producto.Id),
                new SqlParameter("@nombre", producto.nombre),
                new SqlParameter("@precio", producto.precio),
                new SqlParameter("@id_categoria", producto.id_categoria),
                new SqlParameter("@usuario_modificacion", producto.Usuario_Creacion) // Asumiendo que se usa el mismo campo para la modificación
            };
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_Productos_Modificar @id, @nombre, @precio, @id_categoria, @usuario_modificacion",
                    parameters);
            }
            catch (Exception ex)
            {
                // Considera usar un logger en lugar de Console.WriteLine en producción
                throw new Exception("Error al modificar el producto", ex);
            }
        }
        public async Task EliminarProducto(int id, string usuarioModificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@usuario_modificacion", usuarioModificacion)
            };
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_Productos_Eliminar @id, @usuario_modificacion",
                    parameters);
            }
            catch (Exception ex)
            {
                // Considera usar un logger en lugar de Console.WriteLine en producción
                throw new Exception("Error al eliminar el producto", ex);
            }
        }
    }
}


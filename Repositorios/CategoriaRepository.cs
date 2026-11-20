using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios
{
    public class CategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearCategoria(Categoria categoria)
        {
            var parameters = new[]
            {
                new SqlParameter("@nombre", categoria.Nombre),
                new SqlParameter("@usuario_creacion", categoria.Usuario_Creacion)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_Categorias_Insertar @nombre, @usuario_creacion",
                    parameters);
            }
            catch (Exception ex)
            {
                // Considera usar un logger en lugar de Console.WriteLine en producción
                throw new Exception("Error al insertar la categoría", ex);
            }
        }

        public async Task<List<Categoria>> LeerCategorias()
        {
            // Usamos el procedimiento sp_Categorias_Consultar sin parámetros para listar todos
            return await _context.Categorias.FromSqlRaw("EXEC sp_Categorias_Consultar").ToListAsync();
        }

        public async Task<Categoria> BuscarCategoriaPorId(int id)
        {
            var parameter = new SqlParameter("@id", id);
            var result = await _context.Categorias
                .FromSqlRaw("EXEC sp_Categorias_Consultar @id", parameter)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task ModificarCategoria(Categoria categoria)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", categoria.Id),
                new SqlParameter("@nombre", categoria.Nombre),
                new SqlParameter("@usuario_modificacion", categoria.Usuario_Modificacion)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Categorias_Actualizar @id, @nombre, @usuario_modificacion",
                parameters);
        }

        public async Task EliminarCategoria(int id, string usuarioModificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@usuario_modificacion", usuarioModificacion)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Categorias_Eliminar @id, @usuario_modificacion",
                parameters);
        }
    }
}

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios
{
    public class EmisorRepositorio
    {
        private readonly AppDbContext _context;

        public EmisorRepositorio(AppDbContext context)
        {
            _context = context;
        }
        public async Task CrearEmisor(Emisor emisor)
        {
            var parameters = new[]
            {
                new SqlParameter("@razon_social", emisor.razon_social),
                new SqlParameter("@usuario_creacion", emisor.Usuario_Creacion)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_Emisor_Insertar @razon_social, @usuario_creacion",
                    parameters);
            }
            catch (Exception ex)
            {
                // Considera usar un logger en lugar de Console.WriteLine en producción
                throw new Exception("Error al insertar el emisor", ex);
            }
        }

        public async Task<List<Emisor>> LeerEmisor()
        {
            // Usamos el procedimiento sp_Categorias_Consultar sin parámetros para listar todos
            return await _context.Emisores.FromSqlRaw("EXEC sp_Emisor_Consultar").ToListAsync();
        }

        public async Task<Emisor?> BuscarEmisorPorId(int id)
        {
            var parameter = new SqlParameter("@id", id);
            var result = await _context.Emisores
                .FromSqlRaw("EXEC sp_Emisor_Consultar @id", parameter)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task ModificarEmisor(Emisor emisor)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", emisor.id),
                new SqlParameter("@razon_social", emisor.razon_social),
                new SqlParameter("@usuario_modificacion", emisor.Usuario_Modificacion)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Emisor_Actualizar @id, @razon_social, @usuario_modificacion",
                parameters);
        }

        public async Task EliminarEmisor(int id, string usuarioModificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@usuario_modificacion", usuarioModificacion)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Emisor_Eliminar @id, @usuario_modificacion",
                parameters);
        }
    }
}

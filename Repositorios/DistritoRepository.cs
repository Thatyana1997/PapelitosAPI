using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios
{
    public class DistritoRepository
    {
        private readonly AppDbContext _context;



        public DistritoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearDistrito(Distrito distrito)
        {
            var parameters = new[]
            {
                new SqlParameter("@nombre", distrito.Nombre),
                new SqlParameter("@usuario_creacion", distrito.Usuario_Creacion)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_Distrito_Insertar @nombre, @usuario_creacion",
                    parameters);
            }
            catch (Exception ex)
            {
                // Considera usar un logger en lugar de Console.WriteLine en producción
                throw new Exception("Error al insertar el distrito", ex);
            }
        }

        public async Task<List<Distrito>> LeerDistrito()
        {
            // Usamos el procedimiento sp_Distrito_Consultar sin parámetros para listar todos
            return await _context.Distrito.FromSqlRaw("EXEC sp_Distrito_Consultar").ToListAsync();
        }

        public async Task<Distrito> BuscarDistritoPorId(int id)
        {
            var parameter = new SqlParameter("@id", id);
            var result = await _context.Distrito
                .FromSqlRaw("EXEC sp_Distrito_Consultar @id", parameter)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task ModificarDistrito(Distrito distrito)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", distrito.Id),
                new SqlParameter("@nombre", distrito.Nombre),
                new SqlParameter("@usuario_modificacion", distrito.Usuario_Modificacion)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Distrito_Actualizar @id, @nombre, @usuario_modificacion",
                parameters);
        }

        public async Task EliminarDistrito(int id, string usuarioModificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@usuario_modificacion", usuarioModificacion)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Distrito_Eliminar @id, @usuario_modificacion",
                parameters);
        }
    }
}

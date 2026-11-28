using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios
{
    public class ProvinciaRepository
    {
        private readonly AppDbContext _context;

        public ProvinciaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CrearProvincia(Provincia provincia)
        {
            var parameters = new[]
            {
                new SqlParameter("@nombre", provincia.Nombre),
                new SqlParameter("@usuario_creacion", provincia.Usuario_Creacion)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC sp_Provincia_Insertar @nombre, @usuario_creacion",parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la provincia", ex);
            }
        }


        public async Task<List<Provincia>> LeerProvincias()
        {
            return await _context.Provincias.FromSqlRaw("EXEC sp_Provincia_Consultar").ToListAsync();
        }

        public async Task<Provincia> BuscarProvinciaPorId(int id)
        {
            var parameter = new SqlParameter("@id", id);
            var result = await _context.Provincias.FromSqlRaw("EXEC sp_Provincia_Consultar @id", parameter).ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task ModificarProvincia(Provincia provincia)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", provincia.Id),
                new SqlParameter("@nombre", provincia.Nombre),
                new SqlParameter("@usuario_modificacion", provincia.Usuario_Modificacion)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_Provincia_Actualizar @id, @nombre, @usuario_modificacion",parameters);
        }
        public async Task EliminarProvincia(int id, string usuarioModificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@usuario_modificacion", usuarioModificacion)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_Provincia_Eliminar @id, @usuario_modificacion",parameters);
        }
    }
}

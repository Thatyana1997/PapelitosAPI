using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios
{
    public class TiposIDRepository
    {
        private readonly AppDbContext _context;


        public TiposIDRepository(AppDbContext context)
        {
            _context = context;
        }
             

    public async Task CrearTipoID(TiposID tipoID)
        {
            var parameters = new[]
            {
            new SqlParameter("@nombre", tipoID.Nombre),
            new SqlParameter("@usuario_creacion", tipoID.Usuario_Creacion)
        };
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_TiposID_Insertar @nombre, @usuario_creacion",
                    parameters);
            }
            catch (Exception ex)
            {

                throw new Exception("Error al insertar el Tipo de ID", ex);
            }
        }

        public async Task<List<TiposID>> LeerTiposID()
        {

            return await _context.TiposID.FromSqlRaw("EXEC sp_TiposID_Consultar").ToListAsync();
        }

        public async Task<TiposID> BuscarTipoIDPorId(int id)
        {
            var parameter = new SqlParameter("@id", id);
            var result = await _context.TiposID
                .FromSqlRaw("EXEC sp_TiposID_Consultar @id", parameter)
                .ToListAsync();
            return result.FirstOrDefault();
        }

        public async Task ModificarTipoID(TiposID tipoID)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", tipoID.Id),
                new SqlParameter("@nombre", tipoID.Nombre),
                new SqlParameter("@usuario_modificacion", tipoID.Usuario_Modificacion)
            };
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_TiposID_Modificar @id, @nombre, @usuario_modificacion",
                    parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el Tipo de ID", ex);
            }
        }

        public async Task EliminarTipoID(int id, string usuarioModificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@usuario_modificacion", usuarioModificacion)
            };
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_TiposID_Eliminar @id, @usuario_modificacion",
                    parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el Tipo de ID", ex);
            }
        }
    }
}


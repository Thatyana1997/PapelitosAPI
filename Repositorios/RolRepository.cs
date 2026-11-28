using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios
{
    public class RolRepository
    {
        private readonly AppDbContext _context;

        public RolRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearRol(Rol rol)
        {
            var parameters = new[]
            {
                new SqlParameter("@nombre", rol.Nombre),
                new SqlParameter("@descripcion", (object?)rol.Descripcion ?? DBNull.Value),
                new SqlParameter("@usuario_creacion", rol.Usuario_Creacion)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_Roles_Insertar @nombre, @descripcion, @usuario_creacion",
                    parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el rol", ex);
            }
        }

        public async Task<List<Rol>> LeerRoles()
        {
            return await _context.Roles
                .FromSqlRaw("EXEC sp_Roles_Consultar")
                .ToListAsync();
        }

        public async Task<Rol?> BuscarRolPorId(int id)
        {
            var parameter = new SqlParameter("@id", id);
            var result = await _context.Roles
                .FromSqlRaw("EXEC sp_Roles_Consultar @id", parameter)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task ModificarRol(Rol rol)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", rol.Id),
                new SqlParameter("@nombre", rol.Nombre),
                new SqlParameter("@descripcion", (object?)rol.Descripcion ?? DBNull.Value),
                new SqlParameter("@usuario_modificacion", (object?)rol.Usuario_Modificacion ?? DBNull.Value)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Roles_Actualizar @id, @nombre, @descripcion, @usuario_modificacion",
                parameters);
        }

        public async Task EliminarRol(int id, string usuarioModificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@usuario_modificacion", usuarioModificacion)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Roles_Eliminar @id, @usuario_modificacion",
                parameters);
        }
    }
}
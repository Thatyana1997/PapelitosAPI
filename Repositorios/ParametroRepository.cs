using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios
{
    public class ParametroRepository
    {
        private readonly AppDbContext _context;

        public ParametroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearParametro(Parametro parametro)
        {
            var parameters = new[]
            {
                new SqlParameter("@Clave", parametro.Clave),
                new SqlParameter("@Valor", parametro.Valor),
                new SqlParameter("@UsuarioCreacion", parametro.Usuario_Creacion)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_CrearParametro @Clave, @Valor, @UsuarioCreacion",
                    parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el parámetro", ex);
            }
        }

        public async Task<List<Parametro>> LeerParametros()
        {
            return await _context.Parametros.FromSqlRaw("EXEC sp_ObtenerParametros").ToListAsync();
        }

        public async Task<Parametro> BuscarParametroPorId(int id)
        {
            var parameter = new SqlParameter("@Id", id);
            var result = await _context.Parametros
                .FromSqlRaw("EXEC sp_ObtenerParametros @Id", parameter)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task<List<Parametro>> BuscarParametroPorClave(string clave)
        {
            var parameter = new SqlParameter("@Clave", clave);
            return await _context.Parametros
                .FromSqlRaw("EXEC sp_BuscarParametroPorClave @Clave", parameter)
                .ToListAsync();
        }

        public async Task ModificarParametro(Parametro parametro)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", parametro.Id),
                new SqlParameter("@Clave", parametro.Clave),
                new SqlParameter("@Valor", parametro.Valor),
                new SqlParameter("@UsuarioModificacion", parametro.Usuario_Modificacion)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_ModificarParametro @Id, @Clave, @Valor, @UsuarioModificacion",
                    parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el parámetro", ex);
            }
        }

        public async Task EliminarParametro(int id, string usuarioModificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@UsuarioModificacion", usuarioModificacion)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_EliminarParametro @Id, @UsuarioModificacion",
                    parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el parámetro", ex);
            }
        }
    }
}

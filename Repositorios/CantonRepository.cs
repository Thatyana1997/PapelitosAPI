using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios
{
    public class CantonRepository
    {
        private readonly AppDbContext _context;

        public CantonRepository(AppDbContext context)
        {
            _context = context;
        }

        private static SqlParameter Texto(string nombre, string? valor, int longitud)
        {
            return new SqlParameter(nombre, SqlDbType.NVarChar, longitud)
            {
                Value = (object?)valor ?? DBNull.Value
            };
        }

        public async Task<Canton> CrearCanton(Canton canton)
        {
            var resultado = await _context.Cantones.FromSqlRaw(
                "EXEC dbo.sp_Canton_Insertar @id_provincia, @nombre, @usuario_creacion",
                new SqlParameter("@id_provincia", SqlDbType.Int) { Value = (object?)canton.Id_Provincia ?? DBNull.Value },
                Texto("@nombre", canton.Nombre, 100),
                Texto("@usuario_creacion", canton.Usuario_Creacion, 50))
                .AsNoTracking().ToListAsync();

            return resultado.Single();
        }

        public async Task<List<Canton>> LeerCantones()
        {
            return await _context.Cantones.FromSqlRaw("EXEC dbo.sp_Canton_Consultar")
                .AsNoTracking().ToListAsync();
        }

        public async Task<Canton?> BuscarCantonPorId(int id)
        {
            var resultado = await _context.Cantones.FromSqlRaw(
                "EXEC dbo.sp_Canton_Consultar @id", new SqlParameter("@id", id))
                .AsNoTracking().ToListAsync();
            return resultado.FirstOrDefault();
        }

        public async Task<bool> ModificarCanton(Canton canton)
        {
            var filas = new SqlParameter("@filas", SqlDbType.Int) { Direction = ParameterDirection.Output };
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC dbo.sp_Canton_Actualizar @id, @id_provincia, @nombre, @usuario_modificacion, @filas OUTPUT",
                new SqlParameter("@id", canton.Id),
                new SqlParameter("@id_provincia", SqlDbType.Int) { Value = (object?)canton.Id_Provincia ?? DBNull.Value },
                Texto("@nombre", canton.Nombre, 100),
                Texto("@usuario_modificacion", canton.Usuario_Modificacion, 50), filas);
            return (int)filas.Value > 0;
        }

        public async Task<bool> EliminarCanton(int id, string usuarioModificacion)
        {
            var filas = new SqlParameter("@filas", SqlDbType.Int) { Direction = ParameterDirection.Output };
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC dbo.sp_Canton_Eliminar @id, @usuario_modificacion, @filas OUTPUT",
                new SqlParameter("@id", id), Texto("@usuario_modificacion", usuarioModificacion, 50), filas);
            return (int)filas.Value > 0;
        }
    }
}

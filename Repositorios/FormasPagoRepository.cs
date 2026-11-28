using PapelitosAPI.Modelos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace PapelitosAPI.Repositorios
{
    public class FormasPagoRepository
    {
        private readonly AppDbContext _context;

        public FormasPagoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearFormasPago(FormasPago formasPago)
        {
            var parameters = new[]
            {
                new SqlParameter("@nombre", formasPago.Nombre),
                new SqlParameter("@usuario_creacion", formasPago.Usuario_Creacion)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_Formas_Pago_Insertar @nombre, @usuario_creacion",
                    parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la forma de pago", ex);
            }
        }

        public async Task<List<FormasPago>> LeerFormasPago()
        {
            return await _context.FormasPagos.FromSqlRaw("EXEC sp_Formas_Pago_Consultar").ToListAsync();
        }

        public async Task<FormasPago> BuscarFormaPagoPorId(int id)
        {
            var parameter = new SqlParameter("@id", id);
            var result = await _context.FormasPagos
                .FromSqlRaw("EXEC sp_Formas_Pago_Consultar @id", parameter)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task ModificarFormaPago(FormasPago formasPago)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", formasPago.Id),
                new SqlParameter("@nombre", formasPago.Nombre),
                new SqlParameter("@usuario_modificacion", formasPago.Usuario_Modificacion)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Formas_Pago_Actualizar @id, @nombre, @usuario_modificacion",
                parameters);
        }

        public async Task EliminarFormaPago(int id, string usuarioModificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@usuario_modificacion", usuarioModificacion)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Formas_Pago_Eliminar @id, @usuario_modificacion",
                parameters);
        }
    }
}

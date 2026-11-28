
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios
{
    public class DetalleFacturaRepository
    {
        private readonly AppDbContext _context;

        public DetalleFacturaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearDetalleFactura(DetalleFactura detalle)
        {
            var parameters = new[]
            {
                new SqlParameter("@id_factura", (object?)detalle.id_factura ?? DBNull.Value),
                new SqlParameter("@id_producto", (object?)detalle.id_producto ?? DBNull.Value),
                new SqlParameter("@cantidad", (object?)detalle.cantidad ?? DBNull.Value),
                new SqlParameter("@precio_unitario", (object?)detalle.precio_unitario ?? DBNull.Value),
                new SqlParameter("@usuario_creacion", (object?)detalle.usuario_creacion ?? DBNull.Value)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_DetalleFactura_Insertar @id_factura, @id_producto, @cantidad, @precio_unitario, @usuario_creacion",
                    parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el detalle de factura", ex);
            }
        }

        public async Task<List<DetalleFactura>> LeerDetallesFactura()
        {
            return await _context.DetalleFactura
                .FromSqlRaw("EXEC sp_DetalleFactura_Consultar")
                .ToListAsync();
        }

        public async Task<DetalleFactura?> BuscarDetalleFacturaPorId(int id)
        {
            var parameter = new SqlParameter("@id", id);

            var result = await _context.DetalleFactura
                .FromSqlRaw("EXEC sp_DetalleFactura_Consultar @id", parameter)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task ModificarDetalleFactura(DetalleFactura detalle)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", detalle.Id),
                new SqlParameter("@id_factura", (object?)detalle.id_factura ?? DBNull.Value),
                new SqlParameter("@id_producto", (object?)detalle.id_producto ?? DBNull.Value),
                new SqlParameter("@cantidad", (object?)detalle.cantidad ?? DBNull.Value),
                new SqlParameter("@precio_unitario", (object?)detalle.precio_unitario ?? DBNull.Value),
                new SqlParameter("@usuario_modificacion", (object?)detalle.usuario_modificacion ?? DBNull.Value)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_DetalleFactura_Actualizar @id, @id_factura, @id_producto, @cantidad, @precio_unitario, @usuario_modificacion",
                parameters);
        }

        public async Task EliminarDetalleFactura(int id, string usuarioModificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@usuario_modificacion", usuarioModificacion)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_DetalleFactura_Eliminar @id, @usuario_modificacion",
                parameters);
        }
    }
}

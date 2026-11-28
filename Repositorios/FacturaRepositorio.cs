using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios
{
    public class FacturaRepositorio
    {
        private readonly AppDbContext _context;

        public FacturaRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CrearFactura(Factura factura)
        {
            var idSalida = new SqlParameter("@id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var parameters = new[]
            {
                new SqlParameter("@id_cliente", factura.Id_Cliente),
                new SqlParameter("@id_emisor", factura.Id_Emisor),
                new SqlParameter("@total", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = (object?)factura.Total ?? DBNull.Value },
                new SqlParameter("@usuario_creacion", (object?)factura.Usuario_Creacion ?? DBNull.Value),
                idSalida
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_Facturas_Insertar @id_cliente, @id_emisor, @total, @usuario_creacion, @id OUTPUT",
                    parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la factura", ex);
            }

            return (int)idSalida.Value;
        }

        public async Task<List<Factura>> LeerFacturas()
        {
            return await _context.Facturas.FromSqlRaw("EXEC sp_Facturas_Consultar").ToListAsync();
        }

        public async Task<Factura?> BuscarFacturaPorId(int id)
        {
            var parameter = new SqlParameter("@id", id);
            var result = await _context.Facturas
                .FromSqlRaw("EXEC sp_Facturas_Consultar @id", parameter)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task<int> ModificarFactura(Factura factura)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", factura.Id),
                new SqlParameter("@total", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = (object?)factura.Total ?? DBNull.Value },
                new SqlParameter("@usuario_modificacion", (object?)factura.Usuario_Modificacion ?? DBNull.Value)
            };

            return await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Facturas_Actualizar @id, @total, @usuario_modificacion",
                parameters);
        }

        public async Task<int> EliminarFactura(int id, string usuarioModificacion)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@usuario_modificacion", usuarioModificacion)
            };

            return await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Facturas_Eliminar @id, @usuario_modificacion",
                parameters);
        }
    }
}

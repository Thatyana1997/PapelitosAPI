using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios
{
    public class ClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        // Tipos y longitudes explicitos; los opcionales se envian como NULL de SQL.
        private static SqlParameter Texto(string nombre, int longitud, string? valor) =>
            new(nombre, SqlDbType.NVarChar, longitud) { Value = (object?)valor ?? DBNull.Value };

        private static SqlParameter TipoId(int? valor) =>
            new("@id_tipo_id", SqlDbType.Int) { Value = (object?)valor ?? DBNull.Value };

        public async Task<Cliente> CrearCliente(Cliente cliente)
        {
            var parametros = new[]
            {
                Texto("@nombre", 100, cliente.Nombre), TipoId(cliente.Id_Tipo_Id),
                Texto("@identificacion", 50, cliente.Identificacion),
                Texto("@usuario_creacion", 50, cliente.Usuario_Creacion),
                new SqlParameter("@estado", SqlDbType.Char, 1) { Value = cliente.Estado! }
            };
            // El procedimiento devuelve el id generado y la auditoria real del servidor.
            var resultado = await _context.Clientes.FromSqlRaw(
                "EXEC dbo.sp_Cliente_Insertar @nombre, @id_tipo_id, @identificacion, @usuario_creacion, @estado",
                parametros).AsNoTracking().ToListAsync();
            return resultado.Single();
        }

        public async Task<List<Cliente>> LeerClientes() =>
            await _context.Clientes.FromSqlRaw("EXEC dbo.sp_Cliente_Consultar")
                .AsNoTracking().ToListAsync();

        public async Task<Cliente?> BuscarClientePorId(int id)
        {
            var resultado = await _context.Clientes.FromSqlRaw(
                "EXEC dbo.sp_Cliente_Consultar @id", new SqlParameter("@id", SqlDbType.Int) { Value = id })
                .AsNoTracking().ToListAsync();
            return resultado.SingleOrDefault();
        }

        public async Task<bool> ModificarCliente(Cliente cliente)
        {
            var actualizado = new SqlParameter("@actualizado", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            var parametros = new[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = cliente.Id },
                Texto("@nombre", 100, cliente.Nombre), TipoId(cliente.Id_Tipo_Id),
                Texto("@identificacion", 50, cliente.Identificacion),
                Texto("@usuario_modificacion", 50, cliente.Usuario_Modificacion),
                new SqlParameter("@estado", SqlDbType.Char, 1) { Value = cliente.Estado! },
                actualizado
            };
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC dbo.sp_Cliente_Actualizar @id, @nombre, @id_tipo_id, @identificacion, @usuario_modificacion, @estado, @actualizado OUTPUT",
                parametros);
            return actualizado.Value is bool valor && valor;
        }

        public async Task<bool> EliminarCliente(int id, string usuarioModificacion)
        {
            var actualizado = new SqlParameter("@actualizado", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC dbo.sp_Cliente_Eliminar @id, @usuario_modificacion, @actualizado OUTPUT",
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                Texto("@usuario_modificacion", 50, usuarioModificacion), actualizado);
            return actualizado.Value is bool valor && valor;
        }
    }
}

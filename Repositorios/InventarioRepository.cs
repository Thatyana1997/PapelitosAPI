using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios;

public class InventarioRepository(AppDbContext context)
{
    public Task<List<Inventario>> Consultar(int? id = null) =>
        context.Set<Inventario>()
            .FromSqlRaw("EXEC dbo.sp_ConsultarInventario @id",
                new SqlParameter("@id", SqlDbType.Int) { Value = (object?)id ?? DBNull.Value })
            .AsNoTracking()
            .ToListAsync();

    public async Task<int> Crear(InventarioSolicitud inventario)
    {
        var id = new SqlParameter("@id", SqlDbType.Int) { Direction = ParameterDirection.Output };

        await context.Database.ExecuteSqlRawAsync(
            "EXEC dbo.sp_InsertarInventario @id_producto, @cantidad_existencia, @usuario_creacion, @id OUTPUT",
            new SqlParameter("@id_producto", SqlDbType.Int) { Value = inventario.IdProducto },
            new SqlParameter("@cantidad_existencia", SqlDbType.Int) { Value = inventario.CantidadExistencia },
            new SqlParameter("@usuario_creacion", SqlDbType.NVarChar, 50) { Value = inventario.Usuario.Trim() },
            id);

        return (int)id.Value;
    }

    public async Task<bool> Actualizar(int id, InventarioSolicitud inventario)
    {
        var filas = new SqlParameter("@filas", SqlDbType.Int) { Direction = ParameterDirection.Output };

        await context.Database.ExecuteSqlRawAsync(
            "EXEC dbo.sp_ActualizarInventario @id, @id_producto, @cantidad_existencia, @usuario_modificacion, @filas OUTPUT",
            new SqlParameter("@id", SqlDbType.Int) { Value = id },
            new SqlParameter("@id_producto", SqlDbType.Int) { Value = inventario.IdProducto },
            new SqlParameter("@cantidad_existencia", SqlDbType.Int) { Value = inventario.CantidadExistencia },
            new SqlParameter("@usuario_modificacion", SqlDbType.NVarChar, 50) { Value = inventario.Usuario.Trim() },
            filas);

        return (int)filas.Value > 0;
    }

    public async Task<bool> Eliminar(int id, string usuarioModificacion)
    {
        var filas = new SqlParameter("@filas", SqlDbType.Int) { Direction = ParameterDirection.Output };

        await context.Database.ExecuteSqlRawAsync(
            "EXEC dbo.sp_EliminarInventario @id, @usuario_modificacion, @filas OUTPUT",
            new SqlParameter("@id", SqlDbType.Int) { Value = id },
            new SqlParameter("@usuario_modificacion", SqlDbType.NVarChar, 50) { Value = usuarioModificacion.Trim() },
            filas);

        return (int)filas.Value > 0;
    }
}

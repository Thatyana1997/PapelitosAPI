using PapelitosAPI.Modelos;

namespace PapelitosAPI.Repositorios
{
    public class Telefonorepositorio
    {
        private readonly AppDbContext _context;

        public Telefonorepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearTelefono(Telefono telefono)
        {
            var parameters = new[]
            {
                new SqlParameter("@entidad_id", telefono.entidad_id),
                new SqlParameter("@numero", telefono.numero),
                new SqlParameter("@usuario_creacion", telefono.Usuario_Creacion)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_InsertarTelefono @entidad_id,@numero, @usuario_creacion",
                    parameters);
            }
            catch (Exception ex)
            {
                // Considera usar un logger en lugar de Console.WriteLine en producción
                throw new Exception("Error al insertar la categoría", ex);
            }
        }

        public async Task<List<Telefono>> LeerTelefonos()
        {
            return await _context.Telefonos.FromSqlRaw("EXEC sp_ListarTelefonos").ToListAsync();
        }

        public async Task ModificarTelefono(Telefono telefono)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", telefono.Id),
                new SqlParameter("@entidad_id", telefono.entidad_id),
                new SqlParameter("@numero", telefono.numero),
                new SqlParameter("@usuario_modificacion", telefono.Usuario_Modificacion)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_ActualizarTelefono @id, @entidad_id, @numero, @usuario_modificacion",
                parameters);
        }

        public async Task EliminarTelefono(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", id),
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_EliminarTelefono @id",
                parameters);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos;

namespace PapelitosAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<TiposID> TiposID { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<FormasPago> FormasPagos { get; set; }
        public DbSet<Distrito> Distrito { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Canton> Cantones { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Telefonos> Telefonos { get; set; } //ajuste de la profe
        public DbSet<Emisor> Emisores { get; set; }
        public DbSet<Categoria> Inventario { get; set; }
        public DbSet<DetalleFactura> DetalleFactura { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<TiposID>().ToTable("TiposID");
            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<FormasPago>().ToTable("FormasPago");
            modelBuilder.Entity<Distrito>().ToTable("Distrito");
            modelBuilder.Entity<Rol>().ToTable("Roles");
            modelBuilder.Entity<Parametro>().ToTable("Parametros");
            modelBuilder.Entity<Canton>().ToTable("Canton");
            modelBuilder.Entity<Provincia>().ToTable("Provincia");
            modelBuilder.Entity<Factura>().ToTable("Factura");
            modelBuilder.Entity<Factura>().Property(f => f.Total).HasPrecision(18, 2);
            modelBuilder.Entity<Cliente>().ToTable("Cliente", "dbo");
            modelBuilder.Entity<Telefonos>().ToTable("Telefonos");
            modelBuilder.Entity<Emisor>().ToTable("Emisor");
            modelBuilder.Entity<Inventario>().ToTable("Inventario");//tuve que completar 
            modelBuilder.Entity<DetalleFactura>().ToTable("DetalleFactura");
        }

    }
}

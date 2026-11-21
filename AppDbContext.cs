using Microsoft.EntityFrameworkCore;
using PapelitosAPI.Modelos; 

namespace PapelitosAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<TiposID> TiposID { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<TiposID>().ToTable("TiposID");
        }

    }
}

using Microsoft.EntityFrameworkCore;
using Valkimia.ABMClientes.Models;

namespace Valkimia.ABMClientes.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Ciudad)
                .WithMany(ci => ci.Clientes)
                .HasForeignKey(c => c.CiudadId);

            modelBuilder.Entity<Factura>()
                .Property(i => i.Importe)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Cliente)
                .WithMany(c => c.Facturas)
                .HasForeignKey(f => f.ClienteId);
        }
        

    }
}

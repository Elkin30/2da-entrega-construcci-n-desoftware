using Microsoft.EntityFrameworkCore;
using ApiDescuentosCobranza.Models;

namespace ApiDescuentosCobranza.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tablas
        public DbSet<Campaña> Campañas { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        // Facturas
        public DbSet<Factura> Facturas { get; set; }

        // Descuentos Aplicados
        public DbSet<DescuentoAplicado> DescuentosAplicados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Precisión decimal Campaña
            modelBuilder.Entity<Campaña>()
                .Property(c => c.PorcentajeDescuento)
                .HasPrecision(5, 2);

            // Precisión decimal Cliente
            modelBuilder.Entity<Cliente>()
                .Property(c => c.SaldoTotal)
                .HasPrecision(18, 2);

            // Precisión decimal Factura
            modelBuilder.Entity<Factura>()
                .Property(f => f.Valor)
                .HasPrecision(18, 2);

            // Precisión decimal DescuentoAplicado
            modelBuilder.Entity<DescuentoAplicado>()
                .Property(d => d.ValorDescuento)
                .HasPrecision(18, 2);

            modelBuilder.Entity<DescuentoAplicado>()
                .Property(d => d.IVA)
                .HasPrecision(18, 2);
            
            modelBuilder.Entity<DescuentoAplicado>()
                .Property(d => d.TotalNotaCredito)
                .HasPrecision(18, 2);

            // Relación Usuario -> Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId);
        }
    }
}
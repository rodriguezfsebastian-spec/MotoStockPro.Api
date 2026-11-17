using Microsoft.EntityFrameworkCore;
using MotoStockPro.Api.Models;

namespace MotoStockPro.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Definición de las tablas basadas en las entidades del Grupo 07
        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Existencia> Existencias { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<Traslado> Traslados { get; set; }
        public DbSet<DetalleTraslado> DetalleTraslados { get; set; }
        public DbSet<Ajuste> Ajustes { get; set; }
    }
}
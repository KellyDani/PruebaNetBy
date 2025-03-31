using Microsoft.EntityFrameworkCore;
using NetBy.Core.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Productos> INV_Productos { get; set; }
        public DbSet<Categorias> INV_Categorias { get; set; }
        public DbSet<Ventas> INV_Ventas { get; set; }
        public DbSet<VentasDt> INV_VentasDt { get; set; }
        public DbSet<Compras> INV_Compras { get; set; }
        public DbSet<ComprasDt> INV_ComprasDt { get; set; }
        public DbSet<Stock> INV_Stock { get; set; }
        public DbSet<Kardex> INV_Kardex { get; set; }
    }
}
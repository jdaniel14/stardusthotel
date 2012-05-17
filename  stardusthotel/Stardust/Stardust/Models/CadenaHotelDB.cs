using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Stardust.Models
{
    public class CadenaHotelDB : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ProveedorBean> Proveedor { get; set; }
        public DbSet<ProductoBean> Producto { get; set; }
        public DbSet<Hotel> Hotel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
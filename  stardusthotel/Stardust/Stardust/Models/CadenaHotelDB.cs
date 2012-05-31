using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Stardust.Models;

namespace Stardust.Models
{
    public class CadenaHotelDB : DbContext
    {
        public DbSet<ProductoBean> Producto { get; set; }
        public DbSet<ProveedorBean> Proveedor { get; set; }
        public DbSet<ClienteBean> Cliente { get;  set; }

        public DbSet<TipoHabitacionBean> TipoHabitacion { get; set; }


        public DbSet<PromocionBean> Promociones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<PerfilUsuarioBean> PerfilUsuarioBean { get; set; }

        public DbSet<HotelBean> HotelBean { get; set; }

        public DbSet<EmpleadoBean> EmpleadoBean { get; set; }

        public DbSet<UsuarioBean> UsuarioBean { get; set; }
    }
}
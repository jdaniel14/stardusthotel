using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Prueba.Models
{
    public class PruebaDB : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
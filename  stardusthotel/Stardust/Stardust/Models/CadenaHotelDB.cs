using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Stardust.Models
{
    public class CadenaHotelDB : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
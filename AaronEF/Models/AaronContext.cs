using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AaronContext : DbContext
    {
        public AaronContext():base("name=connSqlServer")
        {
             
        }
        public DbSet<Users> Users { get; set; }
    }
}

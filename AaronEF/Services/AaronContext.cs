using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AaronContext : DbContext
    {
        //试一下直接用conn不加name
        public AaronContext():base("name=connSqlServer")
        {
             
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MovieDbContext : DbContext
    {

        public MovieDbContext() : base("name=DbConn")
        {
        }

        public DbSet<Movies> Movies { get; set; }
    }
}

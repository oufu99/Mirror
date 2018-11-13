using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }

    public class ModelDbContext : DbContext
    {

        public ModelDbContext() : base("name=DbConn")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

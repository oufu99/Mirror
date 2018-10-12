using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EFAndVue.DAL
{
    public class EFCodeFirstDbContext : DbContext
    {

        public EFCodeFirstDbContext() : base("name=MyStrConn")
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teachers> Teachers { get; set; }

    }
}
namespace Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DemoContext : DbContext
    {

        public DemoContext()
            : base("name=MyConn")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        
    }
}
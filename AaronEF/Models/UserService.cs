using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserService
    {
        public static void Insert()
        {
            Users u = new Users() { Age = 11, Name = "testEF" };
            AaronContext context = new AaronContext();
            context.Users.Add(u);
            context.SaveChanges();
        }


    }
}

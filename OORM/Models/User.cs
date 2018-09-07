using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {

        public User()
        {

        }
        public User(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

       
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
    }
}

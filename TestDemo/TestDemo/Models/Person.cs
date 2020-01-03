using IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal class Person
    {
        public string Name { get; set; }
        public void Say()
        {
            Console.WriteLine(Name);
        }
    }



    public class Operate
    {

        private Person p;
        private static object lockObj = new object();

        public Operate()
        {
            if (p == null)
            {
                lock (lockObj)
                {
                    if (p == null)
                    {
                        p = new Person();
                    }
                }
            }
        }

        public void UpdatePersonName(string name)
        {
            p.Name =name;

        }
        public void PersonSay()
        {
            p.Say();
        }
    }
}

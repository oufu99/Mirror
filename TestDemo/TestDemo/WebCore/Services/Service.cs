using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Model;

namespace WebCore.Services
{
    public class Service
    {
        Person _p = null;
        public Service(Person p)
        {
            _p = p;
        }
        public int GetPerson()
        {
            return _p.Age;
        }
    }
}

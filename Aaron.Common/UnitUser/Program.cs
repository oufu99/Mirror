using Aaron.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitUser
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = JwtHelper.IssueJwt();
            JwtHelper.SerializeJWT(token);
            Console.ReadLine();
        }
    }
}

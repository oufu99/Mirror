using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface ILogHelper
    {
        void Info(string msg);

        void Error(string msg);
    }
}

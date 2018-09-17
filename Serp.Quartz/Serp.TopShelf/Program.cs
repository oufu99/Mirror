using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Serp.TopShelf
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
              

                x.RunAsLocalSystem();

                //服务的描述
                x.SetDescription("Topshelf_Description");
                //服务的显示名称
                x.SetDisplayName("Topshelf_DisplayName");
                //服务名称
                x.SetServiceName("Topshelf_ServiceName");

            });
        }
    }
}

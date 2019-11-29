
using ConsoleTest.Models;
using IModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //IServiceLocator locator = new MockServiceLocator(new object[] { new Student(),
            //                                 new NullReferenceException() });

            //IStudent instance = locator.GetInstance<IStudent>();
            //instance.Say();

            //ServiceLocatorProvider provider = new ServiceLocatorProvider(() => locator);

            //ServiceLocator.SetLocatorProvider(provider);
            //var student = ServiceLocator.Current.GetInstance<IStudent>();
            //student.Say();


            IUnityContainer container = new UnityContainer();

            //container.Registrations

            //-----------配置方式注入-----------
            UnityConfigurationSection configuration = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            configuration.Configure(container, "Default");

            //-----------注入服务定位器-----------
            UnityServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);

            IStudent student = locator.GetInstance<IStudent>();
            student.Say();
            Console.ReadLine();
        }
    }



}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aaron.Service;
using IService;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity;
using Unity.Mvc5;

namespace Aaron.Erp.Models
{
    public class BootStrapper
    {
        /// <summary>
        /// 获取容器-注册依赖关系
        /// </summary>
        /// <returns></returns>
        public static IUnityContainer Initialise()
        {
            var container = BulidUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        /// <summary>
        /// 加载容器
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer BulidUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        /// <summary>
        /// 实施依赖注入
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterTypes(IUnityContainer container)
        {
            //类型的配置容器注册
            container.RegisterType<ILogHelper, LogIOHelper>();
            //container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());//通过生命周期实现了单例模式

            //已有对象实例的配置容器注册,也为单例
            //UserService userService = new UserService(); 
            //container.RegisterInstance<IUserService>(userService);

        }
    }
}
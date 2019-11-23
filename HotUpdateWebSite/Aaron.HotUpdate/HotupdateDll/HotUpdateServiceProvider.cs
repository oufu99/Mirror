using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Aaron.HotUpdate
{
    public class HotUpdateServiceProvider : IServiceProvider, ISupportRequiredService, IDisposable
    {
        private IServiceProvider _DefaultServiceProvider = null;
        public HotUpdateContainer Container { get; private set; }
        public HotUpdateServiceProvider(IServiceCollection services, HotUpdateContainer container)
        {
            if (_DefaultServiceProvider == null)
                _DefaultServiceProvider = new DefaultServiceProviderFactory().CreateServiceProvider(services);
            if (Container == null)
            {
                Container = container;
                container.Build();
            }
        }

        public HotUpdateServiceProvider(IServiceScope innserServiceScope, HotUpdateContainer container)
        {
            if (_DefaultServiceProvider == null)
                _DefaultServiceProvider = innserServiceScope.ServiceProvider;
            if (Container == null)
            {
                Container = container;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public object GetRequiredService(Type serviceType)
        {
            var instance = _DefaultServiceProvider.GetRequiredService(serviceType);
            return instance;
        }

        public object GetService(Type serviceType)
        {
            if (_DefaultServiceProvider == null)
                _DefaultServiceProvider = innserServiceScope.ServiceProvider;
            if (Container == null)
            {
                Container = container;
                //Container.RegisterHotUpdateServiceProvider(this);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public object GetRequiredService(Type serviceType)
        {
            var instance = _DefaultServiceProvider.GetRequiredService(serviceType);
            return instance;
        }

        public object GetService(Type serviceType)
        {
            var instance = GetIServiceScopeFactory(serviceType);
            if (instance != null)
            {
                return instance;
            }
            var assModel = Container.GetHotUpdateList().FirstOrDefault(c => c.ServiceType == serviceType);
            if (assModel != null)
            {
                var type = assModel.ImplementationType;
                var objs = new List<object>();
                //我们的类,创建一个对应的实例回去
                //如果有构造函数
                var ctors = type.GetConstructors();
                //约定构造第一个构造函数
                var ctor = ctors.First();
                var parms = ctor.GetParameters();
                foreach (var item in parms)
                {
                    //递归调用
                    objs.Add(GetInstanceParams(item));
                }
                instance = Activator.CreateInstance(type, objs.ToArray());
            }
            else
            {
                //加载mvc自带实例
                instance = _DefaultServiceProvider.GetService(serviceType);

                ////单独处理BaseModel等需要注入的,后面如果有新的,直接在这里加类名就可以了(仅限于Model.dll中的类)
                //var modelDllList = new List<string>() { "BaseModel", "BaseMappingTable" };

                //if (modelDllList.Contains(serviceType.Name))
                //{
                //    //反射出一个BaseModel出去
                //    Assembly assembly = (Assembly)AppDomain.CurrentDomain.GetData("ZP.YMT.Model");
                //    if (assembly == null)
                //    {
                //        byte[] bt = File.ReadAllBytes(HotUpdateHelper.GetAssemblyFullPath("ZP.YMT.Model.dll"));
                //        assembly = Assembly.Load(bt);
                //        AppDomain.CurrentDomain.SetData("ZP.YMT.Model", assembly);
                //    }
                //    //兼容多个Model.dll中的类型 只要加判断就够了
                //    var baseModelType = assembly.GetType("ZP.YMT.Model." + serviceType.Name);
                //    var objs = new List<object>();
                //    //我们的类,创建一个对应的实例回去
                //    //如果有构造函数
                //    var ctors = baseModelType.GetConstructors();
                //    //约定构造第一个构造函数
                //    var ctor = ctors.First();
                //    var parms = ctor.GetParameters();
                //    foreach (var item in parms)
                //    {
                //        //递归调用
                //        objs.Add(GetInstanceParams(item));
                //    }
                //    instance = Activator.CreateInstance(baseModelType, objs.ToArray());

                //}
                //else
                //{
                //    //加载mvc自带实例
                //    instance = _DefaultServiceProvider.GetService(serviceType);
                //}
            }
            return instance;
        }

        /// <summary>
        /// 获取构造函数的参数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object GetInstanceParams(ParameterInfo param)
        {
            var list = new List<object>();
            var type = param.ParameterType;
            //如果这个type是我们的就从我们那里找,不然就用微软的
            var assModellist = Container.GetHotUpdateList();
            var assModel = assModellist.FirstOrDefault(c => c.ServiceType == type);
            if (assModel != null)
            {
                if (assModel.ImplementationObject != null)
                    return assModel.ImplementationObject;

                var ctors = assModel.ImplementationType.GetConstructors();
                var parames = ctors.FirstOrDefault().GetParameters();
                if (parames.Count() == 0)
                {
                    return Activator.CreateInstance(type);
                }
                else
                {
                    foreach (var item in parames)
                    {
                        try
                        {
                            list.Add(GetInstanceParams(item));
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                    return Activator.CreateInstance(assModel.ImplementationType, list.ToArray());
                }
            }
            else
            {
                //加载mvc自带实例
                return _DefaultServiceProvider.GetService(type);
            }
        }

        public object GetIServiceScopeFactory(Type serviceType)
        {
            object instance = null;
            if (serviceType == typeof(IServiceScopeFactory))
            {
                IServiceScopeFactory innerServiceScopeFactory = _DefaultServiceProvider.GetRequiredService<IServiceScopeFactory>();
                return new HotUpdateServiceScopeFactory(innerServiceScopeFactory, Container);
            }

            return instance;
        }

    }
}

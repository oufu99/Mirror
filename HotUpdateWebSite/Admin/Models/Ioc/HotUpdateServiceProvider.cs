using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Admin.Models
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
                        list.Add(GetInstanceParams(item));
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

        //public object GetService(Type serviceType)
        //{
        //    var descriptor = serviceDescriptors.FirstOrDefault(t => t.ServiceType == serviceType);
        //    if (serviceType.Name == "ILogger`1")
        //    {
        //        //ILogger<Microsoft.AspNetCore.Hosting.Internal.web>
        //        descriptor = serviceDescriptors.Where(p => p.ServiceType.Name.IndexOf("ILogger") >= 0).ToArray()[1];
        //        return descriptor.ImplementationInstance;
        //    }

        //    if (descriptor == null)
        //    {
        //        throw new Exception($"服务‘{serviceType.Name}’未注册");
        //    }
        //    else
        //    {
        //        switch (descriptor.Lifetime)
        //        {
        //            case ServiceLifetime.Singleton:
        //                if (SingletonServices.TryGetValue(descriptor.ServiceType, out var obj))
        //                {
        //                    return obj;
        //                }
        //                else
        //                {
        //                    if (descriptor.ImplementationType != null)
        //                    {
        //                        var singletonObject = Activator.CreateInstance(descriptor.ImplementationType);
        //                        SingletonServices.Add(descriptor.ServiceType, singletonObject);
        //                        return singletonObject;
        //                    }
        //                    else if (descriptor.ImplementationInstance != null)
        //                    {
        //                        SingletonServices.Add(descriptor.ServiceType, descriptor.ImplementationInstance);
        //                        return descriptor.ImplementationInstance;
        //                    }
        //                    else if (descriptor.ImplementationFactory != null)
        //                    {
        //                        var singletonObject = descriptor.ImplementationFactory.Invoke(this);
        //                        SingletonServices.Add(descriptor.ServiceType, singletonObject);
        //                        return singletonObject;
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("创建服务失败，无法找到实例类型或实例");
        //                    }
        //                }
        //            case ServiceLifetime.Scoped:
        //                throw new NotSupportedException($"创建失败，暂时不支持 Scoped");
        //            case ServiceLifetime.Transient:
        //                var transientObject = Activator.CreateInstance(descriptor.ImplementationType);
        //                return transientObject;
        //            default:
        //                throw new NotSupportedException("创建失败，不能识别的 LifeTime");
        //        }
        //    }
        //}
    }
}

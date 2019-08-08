using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class HotUpdateServiceScopeFactory : IServiceScopeFactory
    {
        private IServiceScopeFactory _InnerServiceFactory;
        HotUpdateContainer _Container;
        public HotUpdateServiceScopeFactory(IServiceScopeFactory innerServiceFactory, HotUpdateContainer container)
        {
            _InnerServiceFactory = innerServiceFactory;
            _Container = container;
        }

        public IServiceScope CreateScope()
        {
            return new HotUpdateServiceScope(_InnerServiceFactory.CreateScope(), _Container);
        }
    }
}

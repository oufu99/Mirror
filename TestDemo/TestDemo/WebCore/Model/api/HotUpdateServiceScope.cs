using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Model
{
    public class HotUpdateServiceScope : IServiceScope
    {
        private HotUpdateServiceProvider _serviceProvider;

        public HotUpdateServiceScope(IServiceScope innserServiceScope, HotUpdateContainer container)
        {
            _serviceProvider = new HotUpdateServiceProvider(innserServiceScope, container);
        }

        public IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        public void Dispose()
        {
            _serviceProvider.Dispose();
        }
    }
}

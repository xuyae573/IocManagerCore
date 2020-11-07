using Microsoft.Extensions.DependencyInjection;
using System;

namespace IocManagerCore
{
    public class IocManager : IIocManager
    {
        public static IocManager Instance => InstanceLazy.Value;

   
        private static readonly Lazy<IocManager> InstanceLazy = new Lazy<IocManager>(() => new IocManager());

        private IServiceProvider _provider;

        public void SetApplicationServiceProvider(IServiceProvider provider)
        {
            _provider = provider;
        }

 
        private IocManager()
        {

        }

        public TService GetService<TService>()
        {
            return _provider.GetService<TService>();
        }

    }
}

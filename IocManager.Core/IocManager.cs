using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IocManagerCore
{
    public class IocManager : IIocManager
    {
        public static IocManager Instance => InstanceLazy.Value;

       // public ILifetimeScope AutofacContainer { get; set; }


        private static readonly Lazy<IocManager> InstanceLazy = new Lazy<IocManager>(() => new IocManager());

        private IServiceProvider _provider;

        public void SetApplicationServiceProvider(IServiceProvider provider)
        {
            _provider = provider;
        }

 
        private IocManager()
        {

        }

        //public TService Resolve<TService>()
        //{
        //    return AutofacContainer.Resolve<TService>();
        //}

        public TService GetService<TService>()
        {
            return _provider.GetService<TService>();
        }

    }
}

using Autofac;
using System;

namespace IocManagerCore
{
    public interface IIocManager
    {
        //ILifetimeScope AutofacContainer { get; set; }

        /// <summary>
        /// Autofac Container Implmentation
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        //TService Resolve<TService>();

        /// <summary>
        /// Asp.NET Core Implmentation
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService GetService<TService>();
    }
}

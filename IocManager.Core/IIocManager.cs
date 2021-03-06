﻿namespace Microsoft.Extensions.DependencyInjection
{
    public interface IIocManager
    { 
        /// <summary>
        /// Asp.NET Core Implmentation
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService GetService<TService>();
    }
}

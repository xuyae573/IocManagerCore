﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        #region .NET CORE DI
        public static IServiceCollection RegisterAssemblyByConvention(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Count() == 0)
            {
                throw new Exception("assemblies cannot be empty.");
            }
            foreach (var assembly in assemblies)
            {
                RegisterDependenciesByAssembly<ISingletonDependency>(services, assembly);
                RegisterDependenciesByAssembly<ITransientDependency>(services, assembly);
                RegisterDependenciesByAssembly<IScopedDependency>(services, assembly);
            }
            return services;
        }

        public static void RegisterDependenciesByAssembly<TServiceLifetime>(IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes().Where(x => typeof(TServiceLifetime).GetTypeInfo().IsAssignableFrom(x) && x.GetTypeInfo().IsClass && !x.GetTypeInfo().IsAbstract && !x.GetTypeInfo().IsSealed).ToList();
            foreach (var type in types)
            {
                foreach (var interfaceType in GetDefaultServices(type))
                {
                    if (interfaceType != null)
                    {
                        var serviceLifetime = FindServiceLifetime(typeof(TServiceLifetime));
                        services.Add(new ServiceDescriptor(interfaceType, type, serviceLifetime));
                    }
                }
            }
        }

        private static ServiceLifetime FindServiceLifetime(Type type)
        {
            if (type == typeof(ISingletonDependency))
            {
                return ServiceLifetime.Singleton;
            }
            if (type == typeof(ITransientDependency))
            {
                return ServiceLifetime.Transient;
            }
            if (type == typeof(IScopedDependency))
            {
                return ServiceLifetime.Scoped;
            }

            throw new ArgumentOutOfRangeException($"Provided ServiceLifetime type is invalid. Lifetime:{type.Name}");
        }
        #endregion
    
        public static void AddIocManager(this IServiceCollection services)
        {
            services.AddSingleton<IIocManager, IocManager>(provide =>
            {
                return IocManager.Instance;
            });

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.RegisterAssemblyByConvention(assemblies);
        }

        private static List<Type> GetDefaultServices(Type type)
        {
            var serviceTypes = new List<Type>();

            foreach (var interfaceType in type.GetTypeInfo().GetInterfaces())
            {
                var interfaceName = interfaceType.Name;

                if (interfaceName.StartsWith("I"))
                {
                    interfaceName = interfaceName.Substring(1, interfaceName.Length - 1);
                }

                if (type.Name.EndsWith(interfaceName))
                {
                    serviceTypes.Add(interfaceType);
                }
            }

            return serviceTypes;
        }
    }
}

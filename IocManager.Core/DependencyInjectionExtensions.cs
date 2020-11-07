using Autofac;
using Autofac.Extensions.DependencyInjection;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace IocManagerCore
{
    public static class DependencyInjectionExtensions
    {
        public static ContainerBuilder RegisterAssembly(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            if (assemblies.IsNullOrEmpty())
            {
                throw new Exception("assemblies cannot be empty.");
            }
            foreach (var assembly in assemblies)
            {
                RegisterDependenciesByAssembly<ISingletonDependency>(builder, assembly);
                RegisterDependenciesByAssembly<ITransientDependency>(builder, assembly);
                RegisterDependenciesByAssembly<ILifetimeScopeDependency>(builder, assembly);
            }
            return builder;
        }

        private static void RegisterDependenciesByAssembly<TServiceLifetime>(ContainerBuilder builder, Assembly assembly)
        {
            var types = assembly.GetTypes().Where(x => typeof(TServiceLifetime).GetTypeInfo().IsAssignableFrom(x) && x.GetTypeInfo().IsClass && !x.GetTypeInfo().IsAbstract && !x.GetTypeInfo().IsSealed).ToList();
            foreach (var type in types)
            {
                var itype = type.GetTypeInfo().GetInterfaces().FirstOrDefault(x => x.Name.ToUpper().Contains(type.Name.ToUpper()));
                if (itype.IsNull()) continue;
                if (typeof(TServiceLifetime) == typeof(ISingletonDependency))
                {
                    builder.RegisterType(type).As(itype).SingleInstance();
                }
                if (typeof(TServiceLifetime) == typeof(ITransientDependency))
                {
                    builder.RegisterType(type).As(itype).InstancePerDependency();
                }
                if (typeof(TServiceLifetime) == typeof(ILifetimeScopeDependency))
                {
                    builder.RegisterType(type).As(itype).InstancePerLifetimeScope();
                }
            }
        }

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
                RegisterDependenciesByAssembly<ILifetimeScopeDependency>(services, assembly);
            }
            return services;
        }

        public static void RegisterDependenciesByAssembly<TServiceLifetime>(IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes().Where(x => typeof(TServiceLifetime).GetTypeInfo().IsAssignableFrom(x) && x.GetTypeInfo().IsClass && !x.GetTypeInfo().IsAbstract && !x.GetTypeInfo().IsSealed).ToList();
            foreach (var type in types)
            {
                var itype = type.GetTypeInfo().GetInterfaces().FirstOrDefault(x => x.Name.ToUpper().Contains(type.Name.ToUpper()));
                if (itype != null)
                {
                    var serviceLifetime = FindServiceLifetime(typeof(TServiceLifetime));
                    services.Add(new ServiceDescriptor(itype, type, serviceLifetime));
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
            if (type == typeof(ILifetimeScopeDependency))
            {
                return ServiceLifetime.Scoped;
            }

            throw new ArgumentOutOfRangeException($"Provided ServiceLifetime type is invalid. Lifetime:{type.Name}");
        }
        #endregion
        public static void UseIocManager(this IApplicationBuilder app)
        {
            IocManager.Instance.SetApplicationServiceProvider(app.ApplicationServices.GetService<IServiceProvider>());
            //IocManager.Instance.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
        }

        public static void AddIocManager(this IServiceCollection services)
        {
            services.AddSingleton<IIocManager, IocManager>(provide =>
            {
                return IocManager.Instance;
            });
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class AppBuilderExtensions
    {
        public static void UseIocManager(this IApplicationBuilder app)
        {
            IocManager.Instance.SetApplicationServiceProvider(app.ApplicationServices.GetService<IServiceProvider>());
        }
    }
}

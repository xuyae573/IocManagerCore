using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Example
{
    public class DefaultGreetingService : IGreetingService, ISingletonDependency
    {
        public string OperationId { get; set; }

        public DefaultGreetingService()
        {
            OperationId = Guid.NewGuid().ToString()[^4..];
        }
        public string SayHello()
        {
           return $"Operation ID: {OperationId}";
        }
    }
}

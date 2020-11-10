using Microsoft.Extensions.DependencyInjection;

namespace Example
{
    public class GreetingService : DefaultGreetingService, ITransientDependency
    {
        public override string SayHello()
        {
            return $"GreetingService Operation ID: {OperationId}";
        }
    }
}

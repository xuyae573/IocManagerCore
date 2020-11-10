using Microsoft.Extensions.DependencyInjection;

namespace Example
{
    public interface IGreetingService 
    {
        string OperationId { get; set; }
        string SayHello();
    }
}

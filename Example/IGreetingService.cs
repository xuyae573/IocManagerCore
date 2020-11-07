using IocManagerCore;

namespace Example
{
    public interface IGreetingService : ISingletonDependency
    {
        string OperationId { get; set; }
        string SayHello();
    }
}

# IocManagerCore

Using a convention way to register the Service 

for example

Singleton 
```C#
public class DefaultGreetingService : IGreetingService, ISingletonDependency
{
    public virtual string OperationId { get; set; }

    public DefaultGreetingService()
    {
        OperationId = Guid.NewGuid().ToString()[^4..];
    }

    public virtual string SayHello()
    {
       return $"Operation ID: {OperationId}";
    }
}

```


Transient 
```C#
public class DefaultGreetingService : IGreetingService, ITransientDependency
{
    public virtual string OperationId { get; set; }

    public DefaultGreetingService()
    {
        OperationId = Guid.NewGuid().ToString()[^4..];
    }

    public virtual string SayHello()
    {
       return $"Operation ID: {OperationId}";
    }
}

```



Scoped 
```C#
public class DefaultGreetingService : IGreetingService, IScopedDependency
{
    public virtual string OperationId { get; set; }

    public DefaultGreetingService()
    {
        OperationId = Guid.NewGuid().ToString()[^4..];
    }

    public virtual string SayHello()
    {
       return $"Operation ID: {OperationId}";
    }
}

```

Startup Configuration
```C#

public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();

    services.AddIocManager();
}
```

```C#

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
   .....
   app.UseIocManager();
}
```

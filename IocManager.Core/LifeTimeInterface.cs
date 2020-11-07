using System;
using System.Collections.Generic;
using System.Text;

namespace IocManagerCore
{
    public interface ILifetime { }
   
    public interface ISingletonDependency : ILifetime { }
 
    public interface ILifetimeScopeDependency : ILifetime { }
   
    public interface ITransientDependency : ILifetime { }
}

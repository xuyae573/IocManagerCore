using IocManagerCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Example
{
    public class GreetingService : IGreetingService
    {
        public string OperationId { get; set; }

        public GreetingService()
        {
            OperationId = Guid.NewGuid().ToString()[^4..];
        }
        public string SayHello()
        {
            var faces = typeof(IGreetingService).GetInterfaces().FirstOrDefault();
           return $"{faces.Name} - guid: {OperationId}";
        }
    }
}

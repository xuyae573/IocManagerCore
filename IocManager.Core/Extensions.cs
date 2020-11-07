using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace IocManagerCore
{
    public static class Extensions
    {
        public static bool IsNull(this Type type)
        {
            return type == null;
        }
    }
}

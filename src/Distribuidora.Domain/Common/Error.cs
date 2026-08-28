using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Common
{
    public sealed record Error(string Code, string Message)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        
    
    }
}

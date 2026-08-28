using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Users.Login
{
    public sealed record LoginRequest(string Email, string Password);
   
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.API.Users.Login
{
    public sealed record LoginRequest(string Email, string Password);
   
}

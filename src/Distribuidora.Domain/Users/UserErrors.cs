using Distribuidora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;


namespace Distribuidora.Domain.Users
{
    public sealed class UserErrors
    {
        public static readonly Error Blocked = new("User.Blocked", "El usuario esta bloqueado");

        public static readonly Error Suspended = new("User.Suspended", "El usuario esta suspendido");

        public static readonly Error EmailAlreadyExists = new("User.EmailAlreadyExists", "Un usuario con este email ya existe");
    }
}

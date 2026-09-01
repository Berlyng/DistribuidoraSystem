using Distribuidora.Domain.Common;
using Distribuidora.Domain.Users.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Users
{
    public sealed class User: BaseEntity
    {
        public User()
        {
        }

        public User(PersonName name, Email email, PasswordHash passwordHash, UserRole role)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Status = UserStatus.Active;
            Role = role;
        }

        public PersonName Name { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public PasswordHash PasswordHash { get; private set; } = null!;
        public UserStatus Status { get; private set; }
        public UserRole Role { get; private set; }

       public static Result<User> Create(PersonName name, Email email, PasswordHash passwordhash, UserRole role)
        {
            var user = new User(name, email, passwordhash, role);
            return Result<User>.Success(user);
        }

       public Result ChangeEmail(Email email)
        {
            var statusResult = ValidateCanOperate();

            if (statusResult.IsFailure)
            {
                return statusResult;
            }

            Email = email;
            UpdatedAt = DateTime.UtcNow;

            return Result.Success();
        }

        public Result Block()
        {
            if(Status == UserStatus.Blocked)
            {
                return Result.Success();
            }

            Status = UserStatus.Blocked;
            UpdatedAt = DateTime.UtcNow;

            return Result.Success();
        }

        public Result Suspend()
        {
            if (Status == UserStatus.Suspended)
            {
                return Result.Success();
            }

            Status = UserStatus.Suspended;
            UpdatedAt = DateTime.UtcNow;

            return Result.Success();
        }

        public Result Active()
        {
            if(Status == UserStatus.Active)
            {
                return Result.Success();
            }

            Status = UserStatus.Active;
            UpdatedAt = DateTime.UtcNow;

            return Result.Success();
        }



        private Result ValidateCanOperate()
        {
            return Status switch
            {
                UserStatus.Blocked =>
                    Result.Failure(UserErrors.Blocked),

                UserStatus.Suspended =>
                    Result.Failure(UserErrors.Suspended),

                _ =>
                    Result.Success()
            };
        }
    }
}

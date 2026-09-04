using Distribuidora.API.Users.Login;
using Distribuidora.API.Users.Register;
using Distribuidora.Application.Users.Login;
using Distribuidora.Application.Users.Register;
using Distribuidora.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Distribuidora.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<UserRole>(request.Role, ignoreCase: true, out var role))
            {
                return BadRequest(new
                {
                    code = "User.InvalidRole",
                    message = "El rol especificado no es válido."
                });
            }
            var command = new RegisterUserCommand(request.FirstName, request.LastName, request.Email, request.Password, role);
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    code = result.Error.Code,
                    message = result.Error.Message,
                });
            }
            return Created($"/api/users/{result.Value}", new { id = result.Value });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(request.Email, request.Password);
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    code = result.Error.Code,
                    message = result.Error.Message,
                });
            }
            return Ok(result.Value);
        }
    }
}

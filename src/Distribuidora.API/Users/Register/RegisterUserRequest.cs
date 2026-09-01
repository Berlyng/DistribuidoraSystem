namespace Distribuidora.API.Users.Register
{
    public sealed record RegisterUserRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string Role);
    
}

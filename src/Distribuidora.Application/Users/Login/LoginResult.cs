namespace Distribuidora.API.Users.Login
{
    public sealed record LoginResult(Guid Id, 
        string FirstName, 
        string LastName, 
        string Email,
        string Role,
        string AccessToken)
    {
    }
}

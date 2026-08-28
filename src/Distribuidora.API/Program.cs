using Distribuidora.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Distribuidora.Infrastructure;
using Distribuidora.API.Users.Register;
using MediatR;
using Distribuidora.Application.Users.Register;
using Distribuidora.Application;
using Distribuidora.API.Users.Login;
using Distribuidora.Application.Users.Login;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");


app.MapPost("/api/users/register",
    async (
        RegisterUserRequest request,
        ISender sender,
        CancellationToken cancellationToken) =>
    {
        var command = new RegisterUserCommand(request.FirstName, request.LastName, request.Email, request.Password);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Results.BadRequest(new
            {
                code = result.Error.Code,
                message = result.Error.Message,
            });
        }

        return Results.Created($"/api/users/{result.Value}", new { id = result.Value });
    })
    .WithName("RegisterUser")
    .WithTags("Users");


app.MapPost("/api/users/login",
    async (LoginRequest request,
    ISender sender,
    CancellationToken cancellationToken) =>
    {
        var command = new LoginUserCommand(request.Email, request.Password);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Results.BadRequest(new
            {
                code = result.Error.Code,
                message = result.Error.Message,
            });
        }

        return Results.Ok(result.Value);
    })

.WithName("LoginUser")
.WithTags("Users");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

using Distribuidora.API.Users.Login;
using Distribuidora.API.Users.Register;
using Distribuidora.Application;
using Distribuidora.Application.Users.Login;
using Distribuidora.Application.Users.Register;
using Distribuidora.Domain.Users;
using Distribuidora.Infrastructure;
using Distribuidora.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el JWT token"
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var jwtSection = builder.Configuration
    .GetSection("Jwt");

var secretKey = jwtSection["SecretKey"]
    ?? throw new InvalidOperationException(
        "JWT SecretKey is not configured.");

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSection["Issuer"],

                ValidateAudience = true,
                ValidAudience = jwtSection["Audience"],

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey)),

                ClockSkew = TimeSpan.Zero
            };
    });

builder.Services.AddAuthorization();

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


        if (!Enum.TryParse<UserRole>(
        request.Role,
        ignoreCase: true,
        out var role))
        {
            return Results.BadRequest(new
            {
                code = "User.InvalidRole",
                message = "El rol especificado no es válido."
            });
        }

        var command = new RegisterUserCommand(request.FirstName, request.LastName, request.Email, request.Password, role);
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

app.MapGet("/api/test/admin", () =>
{
    return Results.Ok(new
    {
        message = "Administrator access granted."
    });
})
.RequireAuthorization(policy =>
    policy.RequireRole(
        UserRole.Administrator.ToString()))
.WithTags("Test");


app.UseAuthentication();
app.UseAuthorization();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

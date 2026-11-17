using InventoryManagement.API.Middleware;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using InventoryManagement.API.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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
        Description = "Enter: Bearer {your JWT token}"
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
    options.EnableAnnotations();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddApplicationDependencies(
    builder.Configuration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["Jwt:Key"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key!)),

            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var tokenRevocationService =
                    context.HttpContext.RequestServices
                        .GetRequiredService<ITokenRevocationService>();

                var authorizationHeader =
                    context.HttpContext.Request.Headers.Authorization.ToString();

                var token = authorizationHeader
                    .Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase)
                    .Trim();

                if (tokenRevocationService.IsTokenRevoked(token))
                {
                    context.Fail("Token has been revoked.");
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
var app = builder.Build();




app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.Services;

var builder = WebApplication.CreateBuilder(args);

// Database configuration
var dbConfig = builder.Configuration.GetSection("Database");
var csBuilder = new NpgsqlConnectionStringBuilder
{
    Host = dbConfig["Host"] ?? "localhost",
    Port = int.Parse(dbConfig["Port"] ?? "5432"),
    Database = dbConfig["Name"] ?? "seats_reservation",
    Username = Environment.GetEnvironmentVariable("DB_USERNAME") ?? dbConfig["Username"],
    Password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? dbConfig["Password"],
    SearchPath = "base_schema"
};

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(csBuilder.ConnectionString));

// Infrastructure Engine Registrations
builder.Services.AddSingleton<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>(); // Register your new thin Service!

// Domain Application Services
builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddScoped<IHallService, HallService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IPriceCategoryService, PriceCategoryService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<ISessionSeatService, SessionSeatService>();

// JWT Authentication Core Configuration
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = Environment.GetEnvironmentVariable("JWT_SIGNING_KEY") 
                ?? jwtSettings["SigningKey"] 
                // FAIL FAST: App refuses to run without a properly configured key
                ?? throw new InvalidOperationException("Fatal: JWT_SIGNING_KEY environment variable is not configured.");

var keyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Seats Reservation API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var (status, message) = error switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, error.Message),
            
            // Add these two mappings to capture service-layer auth issues centrally:
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, error.Message),
            InvalidOperationException => (StatusCodes.Status409Conflict, error.Message),
            
            _ => (StatusCodes.Status500InternalServerError, "An internal error occurred")
        };
        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(new { error = message });
    });
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
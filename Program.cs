using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Entity Framework and Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JWT Authentication
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});

// Configure Authorization
builder.Services.AddAuthorization();

// Register custom rate-limiting middleware as a singleton
builder.Services.AddSingleton<RateLimitMiddleware>();

// Build and configure the application
var app = builder.Build();

// Use middleware in the HTTP request pipeline
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Add rate-limiting middleware with rate limit set to 5 requests per second
app.UseMiddleware<RateLimitMiddleware>(5, TimeSpan.FromSeconds(1));

// Map controllers
app.MapControllers();

// Run the application
app.Run();


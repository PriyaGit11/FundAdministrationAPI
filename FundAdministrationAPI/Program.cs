using FundAdministrationAPI.Data; 
using FundAdministrationAPI.Middleware; 
using FundAdministrationAPI.Repositories; 
using FundAdministrationAPI.Repositories.Interfaces; 
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog; 
using System.Text; 
 
var builder = WebApplication.CreateBuilder(args); 

// Serilog Configuration 
var logFilePath =
    builder.Configuration["SerilogSettings:LogFilePath"]!;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        logFilePath,
        rollingInterval: RollingInterval.Day)
    .CreateLogger();
 
builder.Host.UseSerilog(); 

// Add Controllers 
builder.Services.AddControllers(); 

// Database 
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseInMemoryDatabase("FundDb")); 

// Dependency Injection 
builder.Services.AddScoped<IFundRepository, FundRepository>(); 
builder.Services.AddScoped<IInvestorRepository, InvestorRepository>(); 
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>(); 

// JWT Authentication 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 
    .AddJwtBearer(options => 
    { 
        options.TokenValidationParameters = 
            new TokenValidationParameters 
            { 
                ValidateIssuer = false, 
                ValidateAudience = false, 
                ValidateLifetime = true, 
                ValidateIssuerSigningKey = true, 
 
                IssuerSigningKey = 
                    new SymmetricSecurityKey( 
                        Encoding.UTF8.GetBytes( 
                            builder.Configuration["Jwt:Key"]!)) 
            }; 
    }); 
 
builder.Services.AddAuthorization(); 

// Monitor API
builder.Services.AddHealthChecks();

// Swagger 
builder.Services.AddEndpointsApiExplorer(); 
 
builder.Services.AddSwaggerGen(options => 
{ 
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Fund Administration API", 
        Version = "v1" 
    }); 
 
    options.AddSecurityDefinition("Bearer", 
        new OpenApiSecurityScheme 
        { 
            Name = "Authorization", 
            Type = SecuritySchemeType.Http, 
            Scheme = "bearer", 
            BearerFormat = "JWT", 
            In = ParameterLocation.Header, 
            Description = "Enter JWT Token" 
        }); 
 
    options.AddSecurityRequirement( 
        new OpenApiSecurityRequirement 
        { 
            { 
                new OpenApiSecurityScheme 
                { 
                    Reference = new OpenApiReference 
                    { 
                        Type = ReferenceType.SecurityScheme, 
                        Id = "Bearer" 
                    } 
                }, 
                new string[] {} 
            } 
        }); 
}); 
 
var app = builder.Build(); 

// Global Exception Middleware 
app.UseMiddleware<ExceptionMiddleware>(); 

// Swagger  
app.UseSwagger(); 
app.UseSwaggerUI(); 

// Serilog Request Logging 
app.UseSerilogRequestLogging();

// Redirect to HTTPS
app.UseHttpsRedirection(); 

// Authentication & Authorization 
app.UseAuthentication(); 
 
app.UseAuthorization(); 

// Controllers 
app.MapControllers(); 
 
// Health Endpoint 
app.MapHealthChecks("/health"); 
 
app.Run();
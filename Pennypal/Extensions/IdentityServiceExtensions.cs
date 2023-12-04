using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Pennypal.Persistence.Data;
using Pennypal.Services;

namespace Pennypal.Extensions;

public static class IdentityServiceExtensions
{
  public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
  {
      
    services.AddIdentityCore<AppUser>(
        options =>
        {
          options.Password.RequireNonAlphanumeric = true;
          options.Password.RequireDigit = true;
          options.Password.RequireLowercase = true;
          options.Password.RequiredLength = 8;
          options.Password.RequireUppercase = true;
          options.SignIn.RequireConfirmedEmail = true;
        }
      ).AddEntityFrameworkStores<AppDbContext>()
        .AddSignInManager<SignInManager<AppUser>>()
        .AddDefaultTokenProviders();
    
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"] ?? string.Empty));
    
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(opt =>
      {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = key,
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero
        };
      });

    services.AddAuthorization();
    services.AddScoped<TokenService>();
    
    return services;
  }
}
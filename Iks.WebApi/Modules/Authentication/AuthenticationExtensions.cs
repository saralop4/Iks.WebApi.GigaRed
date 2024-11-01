using Iks.WebApi.Transversal.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Iks.WebApi.Modules.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

        var appSetingsSection = configuration.GetSection("Config");

        services.Configure<AppSettings>(appSetingsSection);

        var appSettings = appSetingsSection.Get<AppSettings>();

        var key = System.Text.Encoding.UTF8.GetBytes(appSettings.Secret);
        var Issuer = appSettings.Issuer;
        var Audience = appSettings.Audience;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
        })

        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    var userId = int.Parse(context.Principal.Identity.Name); 
                    return Task.CompletedTask; 
                },
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }
                    return Task.CompletedTask;
                }
            };

            options.RequireHttpsMetadata = false;
            options.SaveToken = false;
        });

        return services;

    }
}

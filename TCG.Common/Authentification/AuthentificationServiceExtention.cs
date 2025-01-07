using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace TCG.Common.Authentification;

public static class AuthentificationServiceExtention
{
    public static AuthenticationBuilder ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                //ONLY FOR DEV
                options.RequireHttpsMetadata = false;
                options.Authority = "http://localhost:8080/realms/Tcg-Place-Realm";
                options.Audience = "account";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed.");
                        Console.WriteLine(context.Exception.ToString());
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated.");
                        return Task.CompletedTask;
                    }
                };
            });
    }
}
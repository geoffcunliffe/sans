using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;
using Sans.CreditUnion.API.Infrastructure.Constants;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Services;
using Sans.CreditUnion.Database.Context;
using Sans.CreditUnion.Database.Models;
using System.Reflection;
using System.Text;

namespace Sans.CreditUnion.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHostedService<PaymentPosterService>();

            services.AddDbContext<SansCreditUnionDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<CreditUnionUser>(options => options = OptionConstants.IdentityOptions)
                .AddEntityFrameworkStores<SansCreditUnionDbContext>()
                .AddDefaultTokenProviders()
                .AddRoles<CreditUnionRole>()
                .AddRoleValidator<RoleValidator<CreditUnionRole>>()
                .AddRoleManager<RoleManager<CreditUnionRole>>()
                .AddSignInManager<SignInManager<CreditUnionUser>>();

            services.Scan(scanner => scanner
                .FromAssemblyOf<AccountService>()
                .AddClasses(classes => classes.NotInNamespaceOf(typeof(PaymentPosterService))) // Don't auto register the background job
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Configure<PasswordHasherOptions>(options => options.IterationCount = 50_000);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddMvc(config =>
            {
                // Secure by default - add Authorize Attribute to every endpoint.  Opt-out via [AllowAnonymous] attribute.
                var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUi3(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
                
                settings.PostProcess = document =>
                {
                    document.Info.Title = "SANS Credit Union API";
                    document.Info.Version = "v1";
                    document.Info.Description = "API documentation for managing SANS Credit Union accounts.";
                };
                
                settings.GeneratorSettings.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));
                
                settings.GeneratorSettings.DocumentProcessors.Add(
                    new SecurityDefinitionAppender("JWT token", new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "Copy 'Bearer ' + valid JWT token into field",
                        In = SwaggerSecurityApiKeyLocation.Header
                        
                    }));
            });

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}

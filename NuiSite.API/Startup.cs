using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using NuiSite.API.Entities;

namespace NuiSite.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // add dependency services
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<NuiSiteContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NuiSiteDb")));
            
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStatusCodePages();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();


            //add Jwt Bearer middleware into pipline
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                IncludeErrorDetails = true,
                Authority = "https://securetoken.google.com/nuiweb-69916",
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://securetoken.google.com/nuiweb-69916",
                    ValidateAudience = true,
                    ValidAudience = "nuiweb-69916",
                    ValidateLifetime = true,
                },
            });

            app.UseMvc();
        }
    }
}

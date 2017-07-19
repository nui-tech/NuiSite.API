using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NuiSite.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;

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

            // Add service and create Policy with options
            //https://weblog.west-wind.com/posts/2016/Sep/26/ASPNET-Core-and-CORS-Gotchas
            //https://stackoverflow.com/questions/41796468/failed-http-post-with-angular2-to-asp-net-core-api/41815277#41815277
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials());
            });

            //To get connection string work
            //1 make sure set firewall open for your IP address
            //2 correct connetion's name

            //services.AddDbContext<NuiSiteContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<NuiSiteContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NuiSiteDb")));

            services.AddSingleton<IConfiguration>(Configuration);

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

            // global policy - assign here or on each controller
            app.UseCors("CorsPolicy");

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

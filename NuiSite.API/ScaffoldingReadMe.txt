
ASP.NET MVC core dependencies have been added to the project.
However you may still need to do make changes to your project.

1. Suggested changes to Startup class:
    1.1 Add a constructor:
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
    1.2 Add MVC services:
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
       }

    1.3 Configure web app to use use Configuration and use MVC routing:

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


		Scaffold-DbContext "Server=tcp:nuisite.database.windows.net,1433;Initial Catalog=NuiSite;Persist Security Info=False;User ID=kantae;Password=Nu!575967;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Force
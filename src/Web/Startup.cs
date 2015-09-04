using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;
using NPress.Core;
using NPress.Core.Domains;
using NPress.Core.Identity;
using NPress.Core.Repositories;
using NPress.Core.Services;

namespace NPress.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<GlobalSettings>(Configuration.GetSection("globalSettings"));

            // Options
            services.AddOptions();

            // Settings
            var provider = services.BuildServiceProvider();
            var globalSettings = provider.GetRequiredService<IOptions<GlobalSettings>>().Options;
            services.AddSingleton(s => globalSettings);

            // Repositories
            services.AddSingleton<IPostRepository>(s => new Core.Repositories.Sql.PostRepository(globalSettings));
            services.AddSingleton<IUserRepository>(s => new Core.Repositories.Sql.UserRepository(globalSettings));
            services.AddSingleton<IRoleRepository, Core.Repositories.Memory.RoleRepository>();

            // Identity
            services.AddTransient<ILookupNormalizer, LowerInvariantLookupNormalizer>();
            services.AddTransient<IPasswordHasher<User>, PlaintextPasswordHasher>();
            services.AddIdentity<User, Role>().AddUserStore<UserStore>().AddRoleStore<RoleStore>();

            // Services
            services.AddScoped<IPostService, PostService>();

            // Auth
            services.ConfigureCookieAuthentication(options =>
            {
                options.CookieName = "NPress";
                options.ExpireTimeSpan = new TimeSpan(30, 0, 0, 0);
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = "returnUrl";
                options.LoginPath = options.LogoutPath = new PathString("/admin/login");
                options.AccessDeniedPath = new PathString("/admin/login");
                options.LogoutPath = new PathString("/admin/logout");
            });

            // MVC
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();

            // Configure the HTTP request pipeline.

            // Add the following to the request pipeline only in development environment.
            if(env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseErrorPage();
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }

            // Add localization to the request pipeline.
            app.UseRequestLocalization();

            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add identity to the request pipeline.
            app.UseIdentity();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes
                    .MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}")
                    .MapRoute(
                        name: "adminArea",
                        template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
            });
        }
    }
}

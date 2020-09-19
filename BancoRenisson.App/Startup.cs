using BancoRenisson.Infra.Data.Context;
using Envolva.Infra.CrossCutting.Ioc;
using Envolva.Infra.CrossCutting.Jobs;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace BancoRenisson.App
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
            services.AddCoreModules(Configuration);
            services.AddRazorPages();
            services.AddDbContextPool<ContextMySql>(options => options
                .UseMySql(Configuration.GetConnectionString("Connection"), mySqlOptions =>
                    mySqlOptions.ServerVersion(new Version(8, 0, 18), ServerType.MySql)
            ));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();
            app.UseJobSchedule();
            app.UseHangfireServer();
            UpdateDatabase(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ContextMySql>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
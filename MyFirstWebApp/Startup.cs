using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFirstWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.AutoMapperConfig;

namespace MyFirstWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //services.AddScoped<Microsoft.EntityFrameworkCore.DbContext, DemoContext>();
            services.AddSingleton< DemoContext>();
            services.AddAutoMapper(typeof(MappingProfile));
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DemoContext db )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();// ����������� EndpointRoutingMiddleware

            app.UseAuthorization();// ����������� EndpointMiddleware

            app.UseEndpoints(endpoints =>
            {
                // ����������� ���������
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=AddShops}/{id?}");
               
            });
            ApplyMigrations(db);
            

        }

        private void ApplyMigrations(DbContext db)
        {
           db.Database.Migrate();
        }
    }
}

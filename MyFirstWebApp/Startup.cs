using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFirstWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.AutoMapperConfig;
using Serilog;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;

namespace MyFirstWebApp
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
            services.AddAuthentication("Cookies")
                .AddCookie(options => 
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });
            services.AddAuthorization();
            services.AddMvc();
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "MyFirstWebApp", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;

                //Set the comments path for the swagger json and ui.
                var xmlPath = Path.Combine(basePath, "MyFirstWebApp.xml");
                options.IncludeXmlComments(xmlPath);
                options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"localhost:44391/oauth2/default/v1/authorize"),
                            TokenUrl = new Uri($"localhost:44391/oauth2/default/v1/token"),
                            Scopes = new Dictionary<string, string>
                            {
                            { "openid", "test" },
                            },
                        }
                    },
                });
            });
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

            app.UseStaticFiles();

            app.UseSerilogRequestLogging();
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFirstWebApp v1"));

            app.UseProfileTimeMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();
       
            app.UseEndpoints(endpoints =>
            {
                // определение маршрутов
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=HelloView}/{id?}");
               
            });
            ApplyMigrations(db);
        }

        private void ApplyMigrations(DbContext db)
        {
           db.Database.Migrate();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFirstWebApp.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.AutoMapperConfig;
using Serilog;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddMvc();
            //services.AddScoped<Microsoft.EntityFrameworkCore.DbContext, DemoContext>();
            services.AddSingleton< DemoContext>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddAuthentication()
                .AddCookie(options => 
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                     //ValidateIssuer = true,
                     //ValidIssuer = Configuration["Jwt:Issuer"],
                     //ValidateAudience = true,
                     //ValidAudience = Configuration["Jwt:Audience"],
                     //ValidateLifetime = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                     //ValidateIssuerSigningKey = true,
                    };
                });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "MyFirstWebApp", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;

                //Set the comments path for the swagger json and ui.
                var xmlPath = Path.Combine(basePath, "MyFirstWebApp.xml");
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme.",
                    In = ParameterLocation.Header,
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                       new OpenApiSecurityScheme
                       {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme }
                       },
                       new string[] {}
                    }
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

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebApp.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class UseProfileTimeMiddlewareExtension

    {
        public static IApplicationBuilder UseProfileTimeMiddleware(this IApplicationBuilder application)
        {

            return application.UseMiddleware<ProfileTimeMiddleware>((DemoContext)application.ApplicationServices.GetService(typeof(DemoContext)));
        }
    }

    public class ProfileTimeMiddleware
    {
        
        private readonly RequestDelegate _next;
        private readonly DemoContext _context;
        static TimeSpan timeSpan = TimeSpan.Zero;
        private string parametrs;
        public ProfileTimeMiddleware(RequestDelegate next, DemoContext db)
        {
            _next = next;
            _context = db;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            //read or create new if null DB get by host url and params
            // string URL = context.Request.Query.ToString();
            //  Debug.WriteLine($"URL: {URL}");
            string url = context.Request.Path;
            url = url.ToLower();
            char s= url[url.Length-1];
            if (s == '/'&& url.Length!=1)
            {
                url= url.Substring(0, url.Length-1);
            }
            parametrs = context.Request.QueryString.ToString()?.Replace("?","");

            Debug.WriteLine($"URL: {url}");
            Debug.WriteLine($"parametrs: {parametrs}");


            await this._next(context);
            
            watch.Stop();
            timeSpan = timeSpan.Add(TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds));
            // accumulate data 
            RequestProfile isUrl = _context.Profiles.FirstOrDefault(c => c.ResourceName.ToLower() == url);
            if (isUrl == null)
            {
                RequestProfile requestProfile = new RequestProfile();
                requestProfile.ResourceName = url;
                requestProfile.TotalCount++;
                requestProfile.TotalTime = timeSpan;
                requestProfile.Parametrs = parametrs;

                
                _context.Profiles.Add(requestProfile);
                await _context.SaveChangesAsync();
            }
            else
            {
                isUrl.TotalCount++;
                isUrl.TotalTime += timeSpan;
                _context.Profiles.Update(isUrl);

                await _context.SaveChangesAsync();
            }
            //save data to db 
           
            //Debug.WriteLine($"Total Time: {timeSpan}, Counter: {isUrl.TotalCount} ");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}



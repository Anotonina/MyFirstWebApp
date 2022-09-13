using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Serilog;
using Serilog.Events;

namespace MyFirstWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
           // ����������� ������� ����������� - Debug
           .MinimumLevel.Debug()
           // �������� ���� � ������� ���� Warning ��� ������������ ���� Microsoft.AspNetCore*
           .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
           // ��������� ���������� ������ � ������� LogContext
           .Enrich.FromLogContext()
           // ����� ���� � ������� � �������������� �������
           .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}] {Message:lj}{NewLine}{Exception}")
           .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
       
        
    }
}

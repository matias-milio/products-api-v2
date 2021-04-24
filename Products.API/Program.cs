using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Products.Infrastructure.AppDbContext;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()               
                .WriteTo.Debug(outputTemplate:DateTime.Now.ToString())
                .WriteTo.Seq("http://localhost:5341/")                
                .CreateLogger();

           var host = CreateHostBuilder(args).Build();

            using var enviroment = host.Services.CreateScope();
            var services = enviroment.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<Domain.User>>();
                var context = services.GetRequiredService<MyStoreDbContext>();
                context.Database.Migrate();
                Infrastructure.TestData.Insert(context, userManager).Wait();

            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogCritical(ex, "Migration failed");
            }

            host.Run();
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

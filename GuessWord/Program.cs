using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).Build();

            hostBuilder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                var contentRoot = Directory.GetCurrentDirectory();
                var webRoot = Path.Combine(contentRoot, "wwwroot");

                webBuilder.UseKestrel()
                          .UseIISIntegration()
                          .UseWebRoot(webRoot)
                          .UseStartup<Startup>()
                          .UseUrls("http://*:6060");
            });
    }
}

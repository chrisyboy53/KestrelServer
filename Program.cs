using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace KestrelServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                                .AddCommandLine(args)
                                .Build();

            var webHost = new WebHostBuilder()
                            .UseKestrel()
                            .UseConfiguration(config)
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseStartup<Startup>()
                            .Build();

            Console.WriteLine("Yahoo you've got a web server running");
            webHost.Run();
            Console.WriteLine("Bye bye.");
        }
    }
}

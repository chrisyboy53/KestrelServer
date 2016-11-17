using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace KestrelServer {

    public class Startup : Microsoft.AspNetCore.Hosting.IStartup
    {

        public IConfigurationRoot Configuration { get; }

        public Startup( IHostingEnvironment env )
        {
            Configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
        }

        public void Configure(IApplicationBuilder app)
        {
            var directoryToRead = Configuration["dir"] ?? "/";

            Console.WriteLine(directoryToRead);

            if (directoryToRead == null) {
                return;
            }

            // DirectoryInfo di = new DirectoryInfo(directoryToRead);

            // Configure the services etc here
            // app.UseStaticFiles(new StaticFileOptions() {
            //     FileProvider = new PhysicalFileProvider(
            //         Path.Combine(Directory.GetCurrentDirectory(), @"www")
            //     )
            // });
            var fileProvider = new PhysicalFileProvider(
                    directoryToRead
                );
            app.UseDirectoryBrowser(new DirectoryBrowserOptions() {
                  FileProvider = fileProvider
            });
            app.UseStaticFiles(new StaticFileOptions(){
                FileProvider = fileProvider
            });
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Build your service collection here. DI Magic begins here
            return services
                        .AddDirectoryBrowser()
                        .BuildServiceProvider();
        }
    }

}
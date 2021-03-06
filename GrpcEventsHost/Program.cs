using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace GrpcEventsHost
{
	public class Program
	{

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }


        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    if(env == "Development") 
                    {
                        webBuilder.ConfigureKestrel(options =>
                        {
                            // Setup a HTTP/2 endpoint without TLS. Macos
                            options.ListenLocalhost(7575, o => o.Protocols = HttpProtocols.Http2);
                        });                 
                    }   
                    webBuilder.UseStartup<Startup>();
                });

	}
}

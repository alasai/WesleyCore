using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace WesleyCore.User
{
    /// <summary>
    ///
    /// </summary>
    public class Program
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddCommandLine(args).Build();
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.ConfigureKestrel(options =>
                    //{
                    //    options.Listen(IPAddress.Any, 5003, listenOptions =>
                    //    {
                    //        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    //    });
                    //});
                    webBuilder.UseStartup<Startup>();
                });
    }
}
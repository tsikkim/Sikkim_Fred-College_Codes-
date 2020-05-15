using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SikkimGov.Platform.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseIISIntegration()
                .UseStartup<Startup>();
    }
}

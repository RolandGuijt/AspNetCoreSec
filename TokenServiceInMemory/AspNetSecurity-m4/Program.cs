using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace AspNetSecurity_m4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Website";
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5705")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace AspNetSecurity_m4_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "API";
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5438")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}

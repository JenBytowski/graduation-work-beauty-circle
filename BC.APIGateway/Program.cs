using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BC.APIGateway
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((host, config) =>
        {
          if (!File.Exists("Config/ocelot.json"))
          {
            File.Copy("ocelot.json", "Config/ocelot.json");
          }

          if
          (
            !File.Exists($"Config/ocelot.{host.HostingEnvironment.EnvironmentName}.json") &&
            File.Exists($"ocelot.{host.HostingEnvironment.EnvironmentName}.json")
          )
          {
            File.Copy
            (
              $"ocelot.{host.HostingEnvironment.EnvironmentName}.json",
              $"Config/ocelot.{host.HostingEnvironment.EnvironmentName}.json"
            );
          }

          config
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", true, true)
            .AddJsonFile("Config/ocelot.json")
            .AddJsonFile($"Config/ocelot.{host.HostingEnvironment.EnvironmentName}.json", true, true);
            // .AddJsonFile("ocelot.json")
            // .AddJsonFile($"ocelot.{host.HostingEnvironment.EnvironmentName}.json", true, true);
        })
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
  }
}

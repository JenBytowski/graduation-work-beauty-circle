using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BC.Common
{
  public class Startup
  {
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
      _env = env;
      _configuration = configuration;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();

      var app = Environment.GetEnvironmentVariable("NG_PROJ") ?? "bc-client";
      
      // In production, the Angular files will be served from this directory
      services.AddSpaStaticFiles(configuration => { configuration.RootPath = $"workspace/dist/{app}"; });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      if (!env.IsDevelopment())
      {
        app.UseSpaStaticFiles();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller}/{action=Index}/{id?}");
      });

      app.UseSpa(spa =>
      {
        // To learn more about options for serving an Angular SPA from ASP.NET Core,
        // see https://go.microsoft.com/fwlink/?linkid=864501

        spa.Options.SourcePath = "workspace";

        spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);

        if (env.IsEnvironment("client.local-01"))
        {
          spa.UseAngularCliServer(npmScript: "start-bc-client-local-01");
        }
        else if (env.IsEnvironment("client.local-02"))
        {
          spa.UseAngularCliServer(npmScript: "start-bc-client-local-02");
        }
      });
    }
  }
}

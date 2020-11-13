using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace BC.APIGateway
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    private void AddAuthentication(IServiceCollection services)
    {
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Jwt", opt =>
      {
        opt.RequireHttpsMetadata = false;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidIssuer = "defaultServer",
          ValidAudience = "defaultClient",
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("defaultSecurityKey")),
          ValidateIssuerSigningKey = true
        };
      });
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      this.AddAuthentication(services);
      services.AddCors();
      services.AddControllers();
      services.AddOcelot(Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(builder =>
        builder.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod()
      );

      app.UseHttpsRedirection();

      app.UseRouting();

      // app.UseAuthentication();
      // app.UseAuthorization();

      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
      app.UseOcelot();
    }
  }
}

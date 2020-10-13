using System.Net.Http;
using System.Text;
using BC.API.Events;
using BC.API.Infrastructure.Impl;
using BC.API.Infrastructure.Interfaces;
using BC.API.Services.AuthenticationService.Data;
using AuthenticationService = BC.API.Services.AuthenticationService;
using BC.API.Services.BalanceService;
using BC.API.Services.BalanceService.Data;
using BC.API.Services.MastersListService;
using BC.API.Services.MastersListService.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StrongCode.Seedwork.EventBus;
using StrongCode.Seedwork.EventBus.RabbitMQ;
using JWTokenOptions = BC.API.Services.AuthenticationService.TokenOptions;
using BC.API.Services.BalanceService.Handlers;
using BC.API.Services.BookingService;
using BC.API.Services.BookingService.Data;
using BC.API.Services.MastersListService.Handlers;

namespace BC.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    private void AddEventBus(IServiceCollection services)
    {
      services.AddTransient<ScheduleDayChangedEventHandler>();
      services.AddTransient<UserCreatedHandler>();

      var eb = this.Configuration.GetSection("RabbitMQEventBus");
      services.AddSingleton<IEventBus>(provider =>
      {
        var bus = new RabbitMqEventBus
        (
          eb["Host"],
          eb["UserName"],
          eb["Password"],
          eb["Exchange"],
          provider
        );

        bus.Subscribe<UserCreatedEvent, UserCreatedHandler>();
        bus.Subscribe<ScheduleDayChangedEvent, ScheduleDayChangedEventHandler>();

        return bus;
      });
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      //Swagger
      services.AddSwaggerGen(opt =>
      {
        opt.SwaggerDoc("authentication", new OpenApiInfo {Title = "Authentication", Version = "1.0"});
        opt.SwaggerDoc("masters-list", new OpenApiInfo {Title = "Masters list", Version = "1.0"});
        opt.SwaggerDoc("booking", new OpenApiInfo {Title = "Booking", Version = "1.0"});
        opt.EnableAnnotations();
      });

      services.AddTransient<BalanceService>();
      this.AddEventBus(services);

      services.AddDbContext<AuthenticationContext>
      (opt =>
        {
          opt.UseSqlServer(Configuration.GetConnectionString("AuthenticationContext"));
        },
        ServiceLifetime.Transient,
        ServiceLifetime.Transient
      );

      services.AddDbContext<MastersContext>
      (
        opt =>
        {
          opt.UseSqlServer(Configuration.GetConnectionString("MasterContext"));
        },
        ServiceLifetime.Transient,
        ServiceLifetime.Transient
      );

      services.AddDbContext<BookingContext>(opt =>
        {
          opt.UseSqlServer(Configuration.GetConnectionString("BookingContext"));
        },
        ServiceLifetime.Transient,
        ServiceLifetime.Transient
      );

      services.AddDbContext<BalanceContext>
      (
        opt =>
        {
          opt.UseSqlServer(Configuration.GetConnectionString("BalanceContext"));
        },
        ServiceLifetime.Transient,
        ServiceLifetime.Transient
      );

      services.AddControllers();
      services.AddCors();
      services.AddIdentity<User, Role>().AddEntityFrameworkStores<AuthenticationContext>()
        .AddDefaultTokenProviders();
      services.AddTransient<AuthenticationService.AuthenticationService>();
      services.AddTransient<MastersListService>();
      services.AddTransient<BookingService>();
      services.AddSingleton<ISMSClient, ConsoleSMSClient>();
      services.AddTransient<HttpClient>();
      services.AddTransient<IFilesServiceClient, FilesServiceClient>();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
      {
        var jwtOptions = Configuration.GetSection("JWTokenOptions").Get<JWTokenOptions>();

        opt.RequireHttpsMetadata = false;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidIssuer = jwtOptions.Issuer,
          ValidAudience = jwtOptions.Audience,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),
          ValidateIssuerSigningKey = true
        };
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment() || env.IsEnvironment("Local-01") || env.IsEnvironment("Local-02"))
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(builder =>
        builder.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod()
      );

      //Swagger
      app.UseSwagger();
      app.UseSwaggerUI(opt =>
      {
        opt.SwaggerEndpoint("/swagger/authentication/swagger.json", "Authentication");
        opt.SwaggerEndpoint("/swagger/masters-list/swagger.json", "Masters list");
        opt.SwaggerEndpoint("/swagger/booking/swagger.json", "Booking");
      });

      // app.UseHttpsRedirection();
      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}

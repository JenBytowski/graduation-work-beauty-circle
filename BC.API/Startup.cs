using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using BC.API.Events;
using BC.API.Infrastructure.Impl;
using BC.API.Infrastructure.Interfaces;
using BC.API.Services;
using BC.API.Services.AuthenticationService;
using BC.API.Services.AuthenticationService.Data;
using BC.API.Services.BalanceService;
using BC.API.Services.BalanceService.Data;
using BC.API.Services.BookingService;
using BC.API.Services.BookingService.Data;
using BC.API.Services.MastersListService;
using BC.API.Services.MastersListService.Data;
using BC.API.Services.MastersListService.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StrongCode.Seedwork.EventBus;
using StrongCode.Seedwork.EventBus.RabbitMQ;
using JWTokenOptions = BC.API.Services.AuthenticationService.TokenOptions;

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
      services.AddTransient<BC.API.Services.BalanceService.Handlers.UserAssignedToRoleHandler>();
      services.AddTransient<BC.API.Services.MastersListService.Handlers.UserAssignedToRoleHandler>();

      services.AddTransient<AvatarImageProcessingSaga>();

      var eb = this.Configuration.GetSection("RabbitMQEventBus");
      services.AddSingleton<IEventBus>(provider =>
      {
        var bus = new RabbitMqEventBus
        (
          eb["Host"],
          eb["Port"],
          eb["UserName"],
          eb["Password"],
          eb["Exchange"],
          provider
        );

        bus.Subscribe<UserAssignedToRoleEvent, BC.API.Services.BalanceService.Handlers.UserAssignedToRoleHandler>();
        bus.Subscribe<UserAssignedToRoleEvent, BC.API.Services.MastersListService.Handlers.UserAssignedToRoleHandler>();
        bus.Subscribe<ScheduleDayChangedEvent, ScheduleDayChangedEventHandler>();
        
        bus.Subscribe<AvatarImageProcessingSaga.SagaEvent, AvatarImageProcessingSaga>();

        return bus;
      });
    }

    private void AddApplicationServices(IServiceCollection services)
    {
      services.AddTransient<MastersListServiceConfig>(provider =>
        provider.GetService<IConfiguration>()
          .GetSection("Services:MastersList")
          .Get<MastersListServiceConfig>()
      );
      
      services.AddTransient<BalanceService>();
      services.AddTransient<AuthenticationService>();
      services.AddTransient<MastersListService>();
      services.AddTransient<BookingService>();
    }

    private void AddInfrastructureServices(IServiceCollection services)
    {
      services.AddTransient<HttpClient>();
      services.AddSingleton<ISMSClient, ConsoleSMSClient>();
      services.AddTransient<IFilesServiceClient, FilesServiceClient>();
    }

    private void AddSwagger(IServiceCollection services)
    {
      //Swagger
      services.AddSwaggerGen(opt =>
      {
        opt.SwaggerDoc("authentication", new OpenApiInfo {Title = "Authentication", Version = "1.0"});
        opt.SwaggerDoc("masters-list", new OpenApiInfo {Title = "Masters list", Version = "1.0"});
        opt.SwaggerDoc("booking", new OpenApiInfo {Title = "Booking", Version = "1.0"});
        opt.SwaggerDoc("file-service", new OpenApiInfo {Title = "File service", Version = "1.0"});
        opt.EnableAnnotations();
      });
    }

    private void AddDbConexts(IServiceCollection services)
    {
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
          opt.UseSqlServer(Configuration.GetConnectionString("MastersContext"));
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
    }

    private void AddIdentity(IServiceCollection services)
    {
      services.AddIdentity<User, Role>().AddEntityFrameworkStores<AuthenticationContext>()
        .AddDefaultTokenProviders();
    }

    private void AddAuthentication(IServiceCollection services)
    {
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

    public void ConfigureServices(IServiceCollection services)
    {
      this.AddInfrastructureServices(services);
      this.AddIdentity(services);
      this.AddDbConexts(services);
      this.AddEventBus(services);
      this.AddApplicationServices(services);
      this.AddSwagger(services);
      this.AddAuthentication(services);

      services.AddControllers();
      services.AddCors();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AuthenticationService authenticationService)
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
        opt.SwaggerEndpoint("/swagger/file-service/swagger.json", "File service");
      });

      // app.UseHttpsRedirection();
      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.Use(async (context, next) =>
      {
        try
        {
          await next.Invoke();
        }
        catch (Exception ex)
        {
          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
          context.Response.Headers.Add(new KeyValuePair<string, StringValues>("Content-Type", "application/json"));
          
          await context.Response.WriteAsync(JsonSerializer.Serialize<BadAPIResponse>(new BadAPIResponse
          {
            Messages = new List<string>
            {
              ex.Message,
              ex.InnerException?.Message
            }.Where(mess => !string.IsNullOrEmpty(mess))
          }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }
      });

      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
      
      authenticationService.EnsureDataInitialized().Wait();
    }
  }
}

using BC.API.Infrastructure;
using BC.API.Services.AuthenticationService;
using BC.API.Services.AuthenticationService.AuthenticationContext;
using BC.API.Services.MasterListService;
using BC.API.Services.MasterListService.MasterContext;
using BC.API.Services.SMSService;
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
using System.Text;
using JWTokenOptions = BC.API.Services.TokenOptions;

namespace BC.API
{
  public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Swagger
            services.AddSwaggerGen(opt => opt.SwaggerDoc("v1", new OpenApiInfo {Title = "BC-API", Version = "v1"}));

            services.AddDbContext<AuthenticationContext>( opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("AuthentificationContext"));
            }, ServiceLifetime.Transient);
            services.AddDbContext<MasterContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("AuthentificationContext"));
            }, ServiceLifetime.Transient);

            services.AddControllers();
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthenticationContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<AuthenticationService>();
            services.AddTransient<MasterListService>();
            services.AddSingleton<ISMSClient, ConsoleSMSClient>();

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/swagger/v1/swagger.json", "BC-API"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
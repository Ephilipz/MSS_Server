using DataAccess;
using DataAccess.Logging;
using DataService.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService;
using DataService.Reservation;
using DataAccess.Reservation;

namespace MeetingManagementSystem
{
    public class Startup
    {
        private readonly string AllowCORS = "_AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CORS is used to allow http routes from the client (you would remove this for https website)
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowCORS,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200", "http://192.168.1.6:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                                  });
            });

            ConfigureDI(services);

            //Adds all controllers files and sets all JSON object being received and sent to user Pascal case
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new DefaultNamingStrategy() };
            });

            //Adds swagger which is used to easily test APIs within the website
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeetingManagementSystem", Version = "v1" });
            });

            //Adds user identity with some password options
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
              .AddEntityFrameworkStores<ApplicationContext>();

            //Adds Application context as the DB context which configures the database using the DefualtConnection key in the appsettings.json
            services.AddDbContext<ApplicationContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                        new MySqlServerVersion(new Version(8, 0, 20)))
                .EnableSensitiveDataLogging() // <-- These two calls are optional but help
                .EnableDetailedErrors()       // <-- with debugging (remove for production).
        );


        }

        //this method is used to add all the data services and data accesses as scoped (new instance created for every controller)
        private void ConfigureDI(IServiceCollection services)
        {
            services.AddScoped<ILoggingDataService, LoggingDataService>();
            services.AddScoped<ILoggingDataAccess, LoggingDataAccess>();
            services.AddScoped<IRoomDataService, RoomDataService>();
            services.AddScoped<IRoomDataAccess, RoomDataAccess>();
            services.AddScoped<IReservationDataAccess, ReservationDataAccess>();
            services.AddScoped<IReservationDataService, ReservationDataService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if the environment is on develop, it adds swagger to test the APIs in the browser
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeetingManagementSystem v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowCORS);

            //used for login / registration
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

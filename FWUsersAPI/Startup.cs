using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.SwaggerGen;

using Microsoft.EntityFrameworkCore;

using FWUsersAPI.Entities;
using FWUsersAPI.Repository;
using FWUsersAPI.Services;


namespace FWUsersAPI
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
            services.AddCors(options =>
                            options.AddPolicy("AllowAnyOrigin", builder => builder.AllowAnyOrigin()));
            
            services.AddMvc();

            var connectionString = Configuration["ConnectionStrings:DataAccessPostgreSqlProvider"];
            Console.WriteLine($"db conn string : {connectionString}");

            services.AddDbContext<UserContext>( o => o.UseNpgsql(connectionString, 
                                                                 b => b.MigrationsAssembly("FWUsersAPI")));
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "FWUsers API", Version = "v1", Description = "FWUsers API" });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            app.UseCors("AllowAnyOrigin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            AutoMapper.Mapper.Initialize(cfg => 
            {
                cfg.CreateMap<Entities.User, Models.UserForCreateDto>();
                cfg.CreateMap<Models.UserForCreateDto, Entities.User>();
            });



            app.UseMvc();


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FWUsers API");
                c.ShowRequestHeaders();
            });
        }
    }
}

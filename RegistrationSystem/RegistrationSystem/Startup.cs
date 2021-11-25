using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RegistrationSystem.API.Middlewares;
using RegistrationSystem.BLL.Helpers;
using RegistrationSystem.BLL.Interfases;
using RegistrationSystem.BLL.Services;
using RegistrationSystem.DAL;
using RegistrationSystem.DAL.Entities;
using RegistrationSystem.DAL.Entities.Enums;
using System.Linq;

namespace RegistrationSystem
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RegistrationSystem", Version = "v1" });
            });

            var connectionsString = Configuration["DBConnection"];
            services.AddDbContext<RegistrationSystemContext>(options =>
                options.UseSqlServer(
                    connectionsString,
                    opt => opt.MigrationsAssembly(typeof(RegistrationSystemContext).Assembly.GetName().Name)));

            services.AddTransient<IUserServise, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SeedDb(app);



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RegistrationSystem v1"));
            }

            app.UseMiddleware<GenericExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void SeedDb(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<RegistrationSystemContext>();
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    FirstName = "Super",
                    LastName = "Admin",
                    Login = "admin",
                    Role = Role.Admin,
                    HashedPassword = PasswordHelper.HashPassword("admin")
                });
            }
            context.SaveChanges();
        }
    }
}

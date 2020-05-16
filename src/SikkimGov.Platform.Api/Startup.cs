using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SikkimGov.Platform.Business.Services;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Security;
using SikkimGov.Platform.Common.Security.Contracts;
using SikkimGov.Platform.DataAccess.Repositories;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;

namespace SikkimGov.Platform.Api
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
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDDORepository, DDORepository>();
            services.AddScoped<IDDORegistrationRepository, DDORegistrationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDDORegistraionService, DDORegistraionService>();
            services.AddScoped<IRCORegistrationRepository, RCORegistrationRepository>();
            services.AddScoped<IRCORegistrationService, RCORegistrationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}

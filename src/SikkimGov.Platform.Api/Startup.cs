using System.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SikkimGov.Platform.Business.Services;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.External;
using SikkimGov.Platform.Common.External.Contracts;
using SikkimGov.Platform.Common.Security;
using SikkimGov.Platform.Common.Security.Contracts;
using SikkimGov.Platform.DataAccess.Core;
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
            services.AddDbContext<SikkimFredDbContext>(options => options.UseSqlServer(ConfigurationManager.ConnectionStrings["SikkiFredConnectionString"].ConnectionString));

            services.AddScoped<IDbContext, SikkimFredDbContext>();

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDDORepository, DDORepository>();
            services.AddScoped<IDDORegistrationRepository, DDORegistrationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDDORegistraionService, DDORegistraionService>();
            services.AddScoped<IRCORegistrationRepository, RCORegistrationRepository>();
            services.AddScoped<ISBSPaymentRepository, SBSPaymentRepository>();
            services.AddScoped<ISBSReceiptRepository, SBSReceiptRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();

            services.AddScoped<IRCORegistrationService, RCORegistrationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISBSFileService, SBSFileService>();
            services.AddScoped<IFeedbackService, FeedbackService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

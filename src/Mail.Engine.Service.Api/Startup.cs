using HealthChecks.UI.Client;
using Mail.Engine.Service.Api.dto;
using Mail.Engine.Service.Api.Exceptions;
using Mail.Engine.Service.Api.Services;
using Mail.Engine.Service.Application.Configuration;
using Mail.Engine.Service.Application.Handlers;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Services;
using Mail.Engine.Service.Core.Services.Mail;
using Mail.Engine.Service.Core.Services.Mail.InboundMail;
using Mail.Engine.Service.Core.Services.Mail.OutboundMail;
using Mail.Engine.Service.Core.Services.Wati;
using Mail.Engine.Service.Infrastructure.Repositories;
using Mail.Engine.Service.Infrastructure.Services;
using Mail.Engine.Service.Infrastructure.Services.InboundMail;
using Mail.Engine.Service.Infrastructure.Services.OutboundMail;
using Mail.Engine.Service.Infrastructure.Services.Wati;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;


namespace Mail.Engine.Service.Api
{
    public class Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IWebHostEnvironment _env = env;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddControllers();
            services.AddApiVersioning();
            services.AddHealthChecks();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mail.Engine.Service.API", Version = "v1" }); });

            //DI
            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<ApplicationLogs>>();

            services.AddSingleton(typeof(ILogger), logger);
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            // NM: Register the global exception handler
            services.AddSingleton<IExceptionHandler, GlobalExceptionHandler>();

            services.AddAutoMapper(typeof(Startup));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProcessInboundMailHandler).Assembly));

            // Register Background Service
            services.AddHostedService<RecurringTaskService>();

            services.AddMemoryCache();

            services.AddHttpContextAccessor();

            // Common Interfaces
            services.AddScoped<IMailRepository, MailRepository>();
            services.AddScoped<IWatiRepository, WatiRepository>();

            // Component Interfaces
            services.AddScoped<ISqlSelector>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();

                var conString = Environment.GetEnvironmentVariable("PGSQL_CONNECTION_STRING")
                    ?? configuration.GetConnectionString("PGSQL_CONNECTION_STRING");

                return new SqlSelector(conString!);
            });

            // Inbound Mail Service
            services.AddScoped<IInboundMailService, InboundMailService>();

            services.AddScoped<IEmailProcessor, StandardEmailProcessor>();

            services.AddScoped<IAttachmentProcessor, AttachmentProcessor>();
            services.AddScoped<IEmailAuthenticator, OutlookAuthenticator>();
            services.AddScoped<IEmailAuthenticator, StandardAuthenticator>();
            services.AddScoped<IEmailFolderManager, EmailFolderManager>();
            services.AddScoped<IMailMessageBuilder, MailMessageBuilder>();

            // Outbound Mail Service
            services.AddScoped<IOutboundMailService, OutboundMailService>();

            services.AddScoped<IEmailAttachmentProcessor, EmailAttachmentProcessor>();
            services.AddScoped<IEmailBuilder, EmailBuilder>();
            services.AddScoped<ISmtpClientFactory, SmtpClientFactory>();

            // Wati Service
            services.AddScoped<IWatiService, WatiService>();

            services.AddSingleton<IConfigurationService, ConfigurationService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mail.Engine.Service.API v1"));
            }

            app.UseExceptionHandler("/error");
            app.UseAuthentication();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseCors("ApiCorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}

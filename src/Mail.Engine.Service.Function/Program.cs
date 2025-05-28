using Mail.Engine.Service.Application.Configuration;
using Mail.Engine.Service.Application.Handlers;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Services;
using Mail.Engine.Service.Core.Services.Mail;
using Mail.Engine.Service.Core.Services.Mail.InboundMail;
using Mail.Engine.Service.Core.Services.Mail.OutboundMail;
using Mail.Engine.Service.Core.Services.Wati;
using Mail.Engine.Service.Function.dto;
using Mail.Engine.Service.Function.Middleware;
using Mail.Engine.Service.Infrastructure.Repositories;
using Mail.Engine.Service.Infrastructure.Services;
using Mail.Engine.Service.Infrastructure.Services.InboundMail;
using Mail.Engine.Service.Infrastructure.Services.OutboundMail;
using Mail.Engine.Service.Infrastructure.Services.Wati;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// var builder = FunctionsApplication.CreateBuilder(args);

var host = new HostBuilder()
   .ConfigureFunctionsWebApplication(worker =>
   {
      worker.UseMiddleware<ErrorHandlerMiddleware>();
      //x.UseMiddleware<AuthorizationMiddleware>();
      // worker.UseNewtonsoftJson(); // Removed: Not available in isolated worker
   })
   // .ConfigureOpenApi()
   .ConfigureServices(services =>
   {
      services.AddApplicationInsightsTelemetryWorkerService();
      services.ConfigureFunctionsApplicationInsights();

      services.AddHttpClient();

      // Just add these instead:
      services.AddLogging();
      services.AddSingleton<ILoggerFactory, LoggerFactory>();
      services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

      services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProcessInboundMailHandler).Assembly));

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

   })
   .Build();

host.Run();

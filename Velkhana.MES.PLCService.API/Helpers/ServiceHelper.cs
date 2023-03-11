using System.Reflection;
using FluentValidation;
using MediatR;
using Velkhana.MES.PLCService.Application;
using Velkhana.Shared.WebServer.Helpers;
using Velkhana.MES.PLCService.Infrastructure;

namespace Velkhana.MES.PLCService.API.Helpers;

public static class ServiceHelper
{
  public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
  {
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services
      .RegisterMainServices(connectionString)
      .RegisterAdditionalServices();
    return builder;
  }

  public static IServiceCollection RegisterMainServices(this IServiceCollection services, string cnnString)
  {
    services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .RegisterApplicationTypes()
    .RegisterModuleServices(cnnString)
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DI).Assembly));

    return services;
  }

  public static IServiceCollection RegisterAdditionalServices(this IServiceCollection services)
  {
    services.RegisterAllWebAPIEndpoints();
    return services;
  }
}

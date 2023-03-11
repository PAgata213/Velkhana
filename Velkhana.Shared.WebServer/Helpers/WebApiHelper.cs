using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Velkhana.Shared.WebServer.Interfaces;

namespace Velkhana.Shared.WebServer.Helpers;
public static class WebApiHelper
{
  public static IServiceCollection RegisterAllWebAPIEndpoints(this IServiceCollection serviceCollection)
  {
    var webApiType = typeof(IWebAPI);
    var types = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(webApiType.IsAssignableFrom)
        .Where(s => !s.IsInterface);

    foreach(var type in types)
    {
      serviceCollection.AddScoped(webApiType, type);
    }

    return serviceCollection;
  }

  public static WebApplication MapAllWebAPIEndpoints(this WebApplication app)
  {
    using(var scope = app.Services.CreateScope())
    {
      var services = scope.ServiceProvider.GetServices<IWebAPI>();

      foreach(var service in services)
      {
        service.MapEndpoints(app);
      }
    }
    return app;
  }
}

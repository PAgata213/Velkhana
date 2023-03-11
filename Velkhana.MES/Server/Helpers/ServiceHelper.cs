using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Velkhana.MES.Server.Data;
using Velkhana.MES.Server.Models;
using Velkhana.Shared.WebServer.Helpers;
using Velkhana.Shared.WebServer.Interfaces;

namespace Velkhana.MES.Server.Helpers;

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

  private static IServiceCollection RegisterMainServices(this IServiceCollection serviceCollection, string connectionString)
  {
    serviceCollection.AddDbContext<ApplicationDbContext>(options =>
      options.UseSqlServer(connectionString));
    serviceCollection.AddDatabaseDeveloperPageExceptionFilter();

    serviceCollection.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();

    serviceCollection.AddIdentityServer()
        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

    serviceCollection.AddAuthentication()
        .AddIdentityServerJwt();

    serviceCollection.AddEndpointsApiExplorer();
    serviceCollection.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });

    serviceCollection.AddControllersWithViews();
    serviceCollection.AddRazorPages();

    return serviceCollection;
  }

  private static IServiceCollection RegisterAdditionalServices(this IServiceCollection serviceCollection)
  {
    serviceCollection.RegisterAllWebAPIEndpoints();

    return serviceCollection;
  }
}

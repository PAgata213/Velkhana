using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Velkhana.MES.PLCService.Domain.PLC.Aggregates;
using Velkhana.MES.PLCService.Infrastructure.Repositories;

namespace Velkhana.MES.PLCService.Infrastructure;
public static class PLCModule
{
  public static IServiceCollection RegisterModuleServices(this IServiceCollection serviceCollection, string cnnString)
  {
    serviceCollection.AddDbContext<PLCDbContext>(options =>
      options.UseSqlServer(cnnString));
    serviceCollection.AddScoped<IPLCDriverRepository, PLCDriverRepository>();

    return serviceCollection;
  }
}

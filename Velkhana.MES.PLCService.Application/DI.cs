using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Velkhana.MES.PLCService.Application.Common.Behaviors;

namespace Velkhana.MES.PLCService.Application;
public static class DI
{
  public static IServiceCollection RegisterApplicationTypes(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    return serviceCollection;
  }
}

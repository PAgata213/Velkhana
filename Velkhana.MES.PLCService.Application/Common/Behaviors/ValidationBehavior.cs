using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Velkhana.MES.PLCService.Application.Common.Behaviors;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
  where TRequest : IRequest<TResponse> 
  where TResponse : IErrorOr
{
  private readonly IValidator<TRequest>? _validator;

  public ValidationBehavior(IValidator<TRequest>? validator)
  {
    _validator = validator;
  }

  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    if(_validator is null)
    {
      return await next();
    }
    var validationResult = await _validator.ValidateAsync(request, cancellationToken);

    if(validationResult.IsValid)
    {
      return await next();
    }

    var errors = validationResult.Errors.ConvertAll(e => Error.Validation(e.PropertyName, e.ErrorMessage));

    var response = (TResponse?)typeof(TResponse)
                     .GetMethod(name: nameof(ErrorOr<object>.From), bindingAttr: BindingFlags.Static | BindingFlags.Public, types: new[] { typeof(List<Error>) })?
                     .Invoke(null, new[] { errors })!;

    return response;
  }
}

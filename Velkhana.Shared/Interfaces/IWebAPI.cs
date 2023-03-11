using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Velkhana.Shared.Interfaces;
public interface IWebAPI
{
  void RegisterEndpoints(WebApplication app);

  public IResult Problem(IList<Error> errors)
  {
    if(errors.All(e => e.Type == ErrorType.Validation))
    {
      var dict = new Dictionary<string, string[]>();
      foreach(var error in errors)
      {
        dict.Add(error.Code, new[] { error.Description });
      }
      return TypedResults.ValidationProblem(dict);
    }

    var firstError = errors.FirstOrDefault();

    return firstError.Type switch
    {
      ErrorType.Validation => TypedResults.Problem(firstError.Description),
      ErrorType.Conflict => TypedResults.Conflict(firstError.Description),
      ErrorType.NotFound => TypedResults.NotFound(firstError.Description),
      _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
    };
  }
}

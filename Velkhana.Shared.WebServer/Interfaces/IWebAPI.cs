using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Velkhana.Shared.WebServer.Interfaces;
public interface IWebAPI
{
  void MapEndpoints(WebApplication app);

  public static IResult Problem(IList<Error> errors)
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
      ErrorType.Failure => TypedResults.BadRequest(firstError.Description),
      _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
    };
  }

  //public static IResult SendToMediator()
  //{
  //  using(var mediator )
  //  {

  //  }
  //    return 
  //}
}

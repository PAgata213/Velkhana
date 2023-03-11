using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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

  public static async Task<IResult> ForwardRequest(HttpClient httpClient, HttpRequest request, string requestUri)
  {
    var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
    {
      Content = new StreamContent(request.Body)
    };
    var response = await httpClient.SendAsync(requestMessage);
    var content = await response.Content.ReadAsStringAsync();
    return GetResultFromStatusCode(response.StatusCode, content);
  }

  private static IResult GetResultFromStatusCode(HttpStatusCode code, string content)
    => code switch
    {
      HttpStatusCode.OK => TypedResults.Ok(content),
      HttpStatusCode.Created => TypedResults.Created(content),
      HttpStatusCode.Accepted => TypedResults.Accepted(content),
      HttpStatusCode.BadRequest => TypedResults.BadRequest(content),
      HttpStatusCode.Unauthorized => TypedResults.Unauthorized(),
      HttpStatusCode.Forbidden => TypedResults.Forbid(),
      HttpStatusCode.NotFound => TypedResults.NotFound(content),
      HttpStatusCode.Conflict => TypedResults.Conflict(content)
    };
}

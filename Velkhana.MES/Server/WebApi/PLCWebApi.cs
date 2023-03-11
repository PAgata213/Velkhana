using Velkhana.MES.PLCService.Application.PLC.Commands.Create;
using Velkhana.Shared.WebServer.Interfaces;

namespace Velkhana.MES.Server.WebApi;

public class PLCWebApi : IWebAPI
{
  public void MapEndpoints(WebApplication app)
  {
    var plcGroup = app.MapGroup("/plc");

    plcGroup.MapGet("/", GetPLCs);
    plcGroup.MapGet("/{id}", GetPLC);
    plcGroup.MapPost("/create/", CreatePLC);
    plcGroup.MapPut("/update/", UpdatePLC);
    plcGroup.MapPost("/remove/", RemovePLC);

    var plcConnectionGroup = plcGroup.MapGroup("/connection");
    plcGroup.MapPost("/start/", StartPLCConnection);
    plcGroup.MapPost("/stop/", StopPLCConnection);
  }

  public async Task<IResult> GetPLCs()
  {
    return TypedResults.Ok();
  }

  public async Task<IResult> GetPLC(Guid id)
  {
    return TypedResults.Ok();
  }

  public async Task<IResult> CreatePLC(HttpClient httpClient, HttpRequestMessage request, CreatePLCDriverCommand command)
  {
    return TypedResults.Ok();
    //return await httpClient.PostAsJsonAsync("", command);
  }

  public async Task<IResult> UpdatePLC()
  {
    return TypedResults.Ok();
  }

  public async Task<IResult> RemovePLC()
  {
    return TypedResults.Ok();

  }

  public async Task<IResult> StartPLCConnection()
  {
    return TypedResults.Ok();

  }

  public async Task<IResult> StopPLCConnection()
  {
    return TypedResults.Ok();

  }
}

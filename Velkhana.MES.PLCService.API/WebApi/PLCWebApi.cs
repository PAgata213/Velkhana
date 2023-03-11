using MediatR;
using Microsoft.AspNetCore.Mvc;
using Velkhana.MES.PLCService.Application.PLC.Commands.Create;
using Velkhana.MES.PLCService.Application.PLC.Query.All;
using Velkhana.Shared.WebServer.Interfaces;

namespace Velkhana.MES.PLCService.API.WebApi;

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

  public async Task<IResult> GetPLCs([FromServices] IMediator mediator)
  {
    var result = await mediator.Send(new AllPLCDriversQuery());
    return TypedResults.Ok(result);
  }

  public async Task<IResult> GetPLC([FromServices]IMediator mediator, Guid id)
  {
    var result = await mediator.Send(new AllPLCDriversQuery());
    return TypedResults.Ok(result);
  }

  public async Task<IResult> CreatePLC([FromServices]IMediator mediator, CreatePLCDriverCommand command)
  {
    var result = await mediator.Send(command);
    var response = result.Match(s => TypedResults.Created("", s), IWebAPI.Problem);
    return response;
  }

  public async Task<IResult> UpdatePLC([FromServices]IMediator mediator)
  {
    return TypedResults.Ok();
  }

  public async Task<IResult> RemovePLC([FromServices] IMediator mediator)
  {
    return TypedResults.Ok();

  }

  public async Task<IResult> StartPLCConnection([FromServices] IMediator mediator)
  {
    return TypedResults.Ok();

  }

  public async Task<IResult> StopPLCConnection([FromServices] IMediator mediator)
  {
    return TypedResults.Ok();

  }
}

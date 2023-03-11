using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Velkhana.MES.PLCService.Application.PLC.Commands.Create;
using Velkhana.MES.PLCService.Application.PLC.Commands.Delete;
using Velkhana.MES.PLCService.Application.PLC.Commands.Update;
using Velkhana.MES.PLCService.Application.PLC.Query.All;
using Velkhana.MES.PLCService.Application.PLC.Query.Find;
using Velkhana.Shared.WebServer.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    if(!result.Any())
    {
      return TypedResults.NotFound();
    }
    return TypedResults.Ok(result);
  }

  public async Task<IResult> GetPLC([FromServices]IMediator mediator, Guid id)
  {
    var result = await mediator.Send(new FindPLCDriverWithIdQuery { Id = id });
    if(result == null)
    {
      return TypedResults.NotFound();
    }
    return TypedResults.Ok(result);
  }

  public async Task<IResult> CreatePLC([FromServices]IMediator mediator, CreatePLCDriverCommand command)
  {
    var result = await mediator.Send(command);
    var response = result.Match(s => TypedResults.Created("", s), IWebAPI.Problem);
    return response;
  }

  public async Task<IResult> UpdatePLC([FromServices]IMediator mediator, UpdatePLCDriverCommand command)
  {
    var result = await mediator.Send(command);
    var response = result.Match(s => TypedResults.Ok(s), IWebAPI.Problem);
    return response;
  }

  public async Task<IResult> RemovePLC([FromServices] IMediator mediator, DeletePLCDriverCommand command)
  {
    var result = await mediator.Send(command);
    var response = result.Match(s => TypedResults.Ok(), IWebAPI.Problem);
    return response;
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

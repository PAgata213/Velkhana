using ErrorOr;
using MediatR;
using Velkhana.MES.PLCService.Domain.PLC.Aggregates;

namespace Velkhana.MES.PLCService.Application.PLC.Commands.Create;
public record CreatePLCDriverCommand : IRequest<ErrorOr<PLCDriver>>
{
  public string IpAddress { get; set; }
  public int Port { get; set; }
}
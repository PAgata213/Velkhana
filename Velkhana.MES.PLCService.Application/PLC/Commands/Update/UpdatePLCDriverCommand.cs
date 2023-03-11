using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velkhana.MES.PLCService.Domain.PLC.Aggregates;

namespace Velkhana.MES.PLCService.Application.PLC.Commands.Update;
public record UpdatePLCDriverCommand : IRequest<ErrorOr<PLCDriver>>
{
  public required Guid Id { get; init; }
  public string IpAddress { get; set; }
  public int Port { get; set; }
}

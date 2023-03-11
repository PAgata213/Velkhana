using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Velkhana.MES.PLCService.Domain.PLC.Aggregates;

namespace Velkhana.MES.PLCService.Application.PLC.Query.Find;
public record FindPLCDriverWithIdQuery : IRequest<PLCDriver?>
{
  public required Guid Id { get; init; }
}

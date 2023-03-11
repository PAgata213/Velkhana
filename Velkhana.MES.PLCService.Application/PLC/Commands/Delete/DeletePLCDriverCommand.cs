using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Velkhana.MES.PLCService.Application.PLC.Commands.Delete;
public record DeletePLCDriverCommand : IRequest<ErrorOr<bool>>
{
  public required Guid Id { get; init; }
}

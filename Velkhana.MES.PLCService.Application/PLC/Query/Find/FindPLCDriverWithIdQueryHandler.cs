using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Velkhana.MES.PLCService.Domain.PLC.Aggregates;

namespace Velkhana.MES.PLCService.Application.PLC.Query.Find;
internal class FindPLCDriverWithIdQueryHandler : IRequestHandler<FindPLCDriverWithIdQuery, PLCDriver?>
{
  private readonly IPLCDriverRepository _plcDriverRepository;

  public FindPLCDriverWithIdQueryHandler(IPLCDriverRepository plcDriverRepository)
  {
    _plcDriverRepository = plcDriverRepository;
  }
  public async Task<PLCDriver?> Handle(FindPLCDriverWithIdQuery request, CancellationToken cancellationToken) 
  {
    return await _plcDriverRepository.GetByIdAsync(request.Id);
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Velkhana.MES.PLCService.Domain.PLC.Aggregates;

namespace Velkhana.MES.PLCService.Application.PLC.Query.All;
public class AllPLCDriversQueryHandler : IRequestHandler<AllPLCDriversQuery, IList<PLCDriver>>
{
  private readonly IPLCDriverRepository _plcDriverRepository;

  public AllPLCDriversQueryHandler(IPLCDriverRepository plcDriverRepository)
  {
    _plcDriverRepository = plcDriverRepository;
  }
  public async Task<IList<PLCDriver>> Handle(AllPLCDriversQuery request, CancellationToken cancellationToken)
    => await _plcDriverRepository.GetAllAsync();
}

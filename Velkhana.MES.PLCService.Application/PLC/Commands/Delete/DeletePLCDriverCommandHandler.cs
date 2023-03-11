using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Velkhana.MES.PLCService.Domain.PLC.Aggregates;

namespace Velkhana.MES.PLCService.Application.PLC.Commands.Delete;
public class DeletePLCDriverCommandHandler : IRequestHandler<DeletePLCDriverCommand, ErrorOr<bool>>
{
  private readonly IPLCDriverRepository _plcDriverRepository;

  public DeletePLCDriverCommandHandler(IPLCDriverRepository plcDriverRepository)
  {
    _plcDriverRepository = plcDriverRepository;
  }
  public async Task<ErrorOr<bool>> Handle(DeletePLCDriverCommand request, CancellationToken cancellationToken) 
  {
    try
    {
      _plcDriverRepository.RemoveWithId(request.Id);
      await _plcDriverRepository.SaveChangesAsync();
    }
    catch(Exception ex)
    {
      return Error.Failure(description: ex.Message);
    }
    return true;
  }
}

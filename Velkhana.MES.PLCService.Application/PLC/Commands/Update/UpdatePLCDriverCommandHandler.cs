using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velkhana.MES.PLCService.Domain.PLC.Aggregates;

namespace Velkhana.MES.PLCService.Application.PLC.Commands.Update;
public class UpdatePLCDriverCommandHandler : IRequestHandler<UpdatePLCDriverCommand, ErrorOr<PLCDriver>>
{
  private readonly IPLCDriverRepository _plcDriverRepository;

  public UpdatePLCDriverCommandHandler(IPLCDriverRepository plcDriverRepository)
  {
    _plcDriverRepository = plcDriverRepository;
  }
  public async Task<ErrorOr<PLCDriver>> Handle(UpdatePLCDriverCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var plc = await _plcDriverRepository.GetByIdAsync(request.Id);
      if (plc == null)
      {
        return Error.NotFound(description: $"PLC Driver with given Id: {request.Id} does not exists");
      }

      //TODO : pomyslec nad optymalizacja aby nie sprawdzac kazdego parametru a np za pomoca reflekcji wyciagnac wszystkie ktore nalezy zaktualizowac
      if(!string.IsNullOrEmpty(request.IpAddress))
      {
        plc.SetIpAddress(request.IpAddress);
      }

      if(request.Port > 0)
      {
        plc.SetPort(request.Port);
      }

      await _plcDriverRepository.SaveChangesAsync();
      return plc;
    }
    catch(Exception ex)
    {
      return Error.Failure(description: ex.Message);
    }
  }
}

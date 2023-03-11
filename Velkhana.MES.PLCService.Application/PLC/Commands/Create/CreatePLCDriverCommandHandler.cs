using ErrorOr;
using MediatR;
using Velkhana.MES.PLCService.Domain.PLC.Aggregates;

namespace Velkhana.MES.PLCService.Application.PLC.Commands.Create;
public class CreatePLCDriverCommandHandler : IRequestHandler<CreatePLCDriverCommand, ErrorOr<PLCDriver>>
{
  private readonly IPLCDriverRepository _repository;

  public CreatePLCDriverCommandHandler(IPLCDriverRepository repository)
  {
    _repository = repository;
  }
  public async Task<ErrorOr<PLCDriver>> Handle(CreatePLCDriverCommand request, CancellationToken cancellationToken)
  {
    var plc = new PLCDriver
    {
      IpAddress = request.IpAddress,
      Port = request.Port
    };

    if (await _repository.ExistsAsync(plc))
    {
      return Error.Failure(description: "PLC Driver with given parameters already exsits");
    }

    await _repository.AddAsync(plc);

    await _repository.SaveChangesAsync();

    return plc;
  }
}

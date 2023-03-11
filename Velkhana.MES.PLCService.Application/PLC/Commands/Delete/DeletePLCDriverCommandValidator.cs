using FluentValidation;

namespace Velkhana.MES.PLCService.Application.PLC.Commands.Delete;
public class DeletePLCDriverCommandValidator : AbstractValidator<DeletePLCDriverCommand>
{
  public DeletePLCDriverCommandValidator()
  {
    RuleFor(s => s.Id).NotEmpty();
  }
}

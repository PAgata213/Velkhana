using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;

namespace Velkhana.MES.PLCService.Application.PLC.Commands.Create;
public class CreatePLCDriverCommandValidator : AbstractValidator<CreatePLCDriverCommand>
{
  public CreatePLCDriverCommandValidator()
  {
    RuleFor(s => s.IpAddress).NotEmpty().Matches("^((25[0-5]|(2[0-4]|1[0-9]|[1-9]|)[0-9])(\\.(?!$)|$)){4}$");
    RuleFor(s => s.Port).NotNull().GreaterThan(0);
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Velkhana.MES.PLCService.Application.PLC.Query.Find;
internal class FindPLCDriverWithIdQueryValidator : AbstractValidator<FindPLCDriverWithIdQuery>
{
  public FindPLCDriverWithIdQueryValidator()
  {
    RuleFor(s => s.Id).NotNull().NotEmpty();
  }
}

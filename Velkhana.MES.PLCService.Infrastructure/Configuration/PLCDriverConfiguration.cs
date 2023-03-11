using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Velkhana.MES.PLCService.Domain.PLC.Aggregates;

namespace Velkhana.MES.PLCService.Infrastructure.Configuration;
public class PLCDriverConfiguration : IEntityTypeConfiguration<PLCDriver>
{
  public void Configure(EntityTypeBuilder<PLCDriver> builder)
  {
    builder.HasKey(s => s.Id);
    builder.Property(s => s.IpAddress).HasMaxLength(15);
    builder.Property(s => s.Port);
  }
}

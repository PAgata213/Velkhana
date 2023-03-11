using Velkhana.Shared.Domain.Common;

namespace Velkhana.MES.PLCService.Domain.PLC.Aggregates;
public partial class PLCDriver : EntityBase
{
  public required string IpAddress { get; set; }
  public required int Port { get; set; }
}
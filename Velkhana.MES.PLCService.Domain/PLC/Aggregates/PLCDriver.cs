using Velkhana.Shared.Domain.Common;

namespace Velkhana.MES.PLCService.Domain.PLC.Aggregates;
public class PLCDriver : EntityBase
{
  public required string IpAddress { get; set; }
  public required int Port { get; set; }

  public void SetIpAddress(string ipAddress)
    => IpAddress = ipAddress;

  public void SetPort(int port)
    => Port = port;
}
using Velkhana.Shared.Domain.Common;

namespace Velkhana.MES.PLCService.Domain.PLC.Aggregates;
public interface IPLCDriverRepository : IRepository<PLCDriver>
{
  Task<bool> ExistsAsync(PLCDriver plc);
}

using Microsoft.EntityFrameworkCore;
using Velkhana.MES.PLCService.Infrastructure;
using Velkhana.MES.PLCService.Domain.PLC.Aggregates;
using Velkhana.Shared.Infrastructure.Persistence;

namespace Velkhana.MES.PLCService.Infrastructure.Repositories;
internal class PLCDriverRepository : RepositoryBase<PLCDriver>, IPLCDriverRepository
{
  public PLCDriverRepository(PLCDbContext dbContext) : base(dbContext)
  {
  }

  public async Task<bool> ExistsAsync(PLCDriver plc)
    => await ExistsAsync(p => p.IpAddress == plc.IpAddress && p.Port == plc.Port);
}
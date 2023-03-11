using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Velkhana.MES.PLCService.Domain.PLC.Aggregates;

namespace Velkhana.MES.PLCService.Infrastructure;
internal class PLCDbContext : DbContext
{
  public DbSet<PLCDriver> PLCDrivers { get; set; }

  public PLCDbContext(DbContextOptions<PLCDbContext> options) : base(options)
  {
  }

  //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  //{
  //  optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Velkhana.MES.PLCService;Trusted_Connection=True;MultipleActiveResultSets=true");
  //  base.OnConfiguring(optionsBuilder);
  //}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(PLCDbContext).Assembly);

    base.OnModelCreating(modelBuilder);
  }
}

using DigitalSkynet.DotnetCore.DataAccess.StaticData;
using DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess.StaticData;
using Microsoft.EntityFrameworkCore;

namespace DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;

public class SeedableDbContext : DbContext
{
    public SeedableDbContext() { }

    public SeedableDbContext(DbContextOptions<SeedableDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DummyEntity>()
            .HasDataProvider<DummyEntityProvider, DummyEntity>();
        modelBuilder.Entity<DependentDummyEntity>()
            .HasDataProvider<DependentDummyEntityProvider, DependentDummyEntity>();
    }
}

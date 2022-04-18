using Microsoft.EntityFrameworkCore;

namespace DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;

public class DummyDbContext : DbContext
{
    public DummyDbContext() { }

    public DummyDbContext(DbContextOptions<DummyDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DummyEntity>();
    }
}

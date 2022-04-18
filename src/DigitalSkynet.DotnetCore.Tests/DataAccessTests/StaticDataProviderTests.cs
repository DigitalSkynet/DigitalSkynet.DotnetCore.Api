using DigitalSkynet.DotnetCore.TestInfrastructure;
using DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;
using DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess.StaticData;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DigitalSkynet.DotnetCore.Tests.DataAccessTests;

/// <summary>
/// This test is an example of using of database seeders. Please note that no SeedAsync() is overridden here.
/// </summary>
public class StaticDataProviderTests : BaseDbTest<Startup, SeedableDbContext>
{
    [Fact]
    public async Task SeedIsAppliedInTests()
    {
        var dummyEntities = await DbContext.Set<DummyEntity>().OrderBy(x => x.Prop).ToListAsync();
        var seederEntities = DummyEntityProvider.Instance.Entities.ToList();

        Assert.Equal(seederEntities.Count, dummyEntities.Count);

        for (int i = 0; i < dummyEntities.Count; i++)
        {
            Assert.Equal(seederEntities[i].Id, dummyEntities[i].Id);
            Assert.Equal(seederEntities[i].IsDeleted, dummyEntities[i].IsDeleted);
            Assert.Equal(seederEntities[i].Prop, dummyEntities[i].Prop);
        }
    }

    [Fact]
    public async Task CanSeedDependentEntities()
    {
        var dependentDummyEntities = await DbContext.Set<DependentDummyEntity>()
            .Include(x => x.DummyEntity)
            .OrderBy(x => x.Field)
            .ToListAsync();
        var seederEntities = DependentDummyEntityProvider.Instance.Entities.ToList();

        Assert.Equal(seederEntities.Count, dependentDummyEntities.Count);

        for (int i = 0; i < dependentDummyEntities.Count; i++)
        {
            Assert.Equal(seederEntities[i].Id, dependentDummyEntities[i].Id);
            Assert.NotNull(dependentDummyEntities[i].DummyEntity);
            Assert.Equal(dependentDummyEntities[i].DummyEntityId, dependentDummyEntities[i].DummyEntity.Id);
        }

    }
}

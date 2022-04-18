using DigitalSkynet.DotnetCore.DataAccess.StaticData;

namespace DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess.StaticData;

public class DependentDummyEntityProvider : StaticDataProvider<DependentDummyEntityProvider, DependentDummyEntity>
{
    protected override IEnumerable<DependentDummyEntity> GenerateEntities()
    {
        var dummyEntityProvider = DummyEntityProvider.Instance;

        yield return new DependentDummyEntity { Id = Guid.NewGuid(), DummyEntityId = dummyEntityProvider.MainKey, Field = "f1" };
        yield return new DependentDummyEntity { Id = Guid.NewGuid(), DummyEntityId = dummyEntityProvider.MainKey, Field = "f2" };
        yield return new DependentDummyEntity { Id = Guid.NewGuid(), DummyEntityId = dummyEntityProvider.SecondKey, Field = "f3" };
        yield return new DependentDummyEntity { Id = Guid.NewGuid(), DummyEntityId = dummyEntityProvider.SecondKey, Field = "f4" };
        yield return new DependentDummyEntity { Id = Guid.NewGuid(), DummyEntityId = dummyEntityProvider.SecondKey, Field = "f5" };
    }
}

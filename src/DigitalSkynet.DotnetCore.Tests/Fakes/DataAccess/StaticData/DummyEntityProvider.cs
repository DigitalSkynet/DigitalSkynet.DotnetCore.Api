using DigitalSkynet.DotnetCore.DataAccess.StaticData;

namespace DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess.StaticData;

public class DummyEntityProvider : StaticDataProvider<DummyEntityProvider, DummyEntity>
{
    public Guid MainKey { get; } = Guid.NewGuid();
    public Guid SecondKey { get; } = Guid.NewGuid();

    protected override IEnumerable<DummyEntity> GenerateEntities()
    {
        yield return new DummyEntity { Id = MainKey, IsDeleted = false, Prop = "Prop1" };
        yield return new DummyEntity { Id = Guid.NewGuid(), IsDeleted = true, Prop = "Prop2" };
        yield return new DummyEntity { Id = SecondKey, IsDeleted = false, Prop = "Prop3" };
        yield return new DummyEntity { Id = Guid.NewGuid(), IsDeleted = true, Prop = "Prop4" };
        yield return new DummyEntity { Id = Guid.NewGuid(), IsDeleted = false, Prop = "Prop5" };
        yield return new DummyEntity { Id = Guid.NewGuid(), IsDeleted = false, Prop = "Prop6" };
    }
}

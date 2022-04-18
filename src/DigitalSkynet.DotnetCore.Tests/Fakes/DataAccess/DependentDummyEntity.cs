using System.ComponentModel.DataAnnotations;
using DigitalSkynet.DotnetCore.DataStructures.Interfaces;

namespace DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;

public class DependentDummyEntity : IHasKey<Guid>
{
    [Key]
    public Guid Id { get; set; }

    public string Field { get; set; }

    public Guid DummyEntityId { get; set; }
    public virtual DummyEntity DummyEntity { get; set; }
}

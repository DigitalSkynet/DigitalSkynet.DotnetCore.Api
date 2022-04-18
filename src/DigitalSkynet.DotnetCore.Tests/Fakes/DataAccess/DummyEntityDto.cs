using DigitalSkynet.DotnetCore.DataStructures.Interfaces;

namespace DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;

public class DummyEntityDto : ISoftDeletable
{
    public bool IsDeleted { get; set; }
    public string Prop { get; set; }
}

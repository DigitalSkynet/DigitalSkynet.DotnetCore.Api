using System.ComponentModel.DataAnnotations;
using DigitalSkynet.DotnetCore.DataStructures.Interfaces;

namespace DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;

public class DummyEntityVm : ISoftDeletable, IHasKey<Guid>, ITimestamped
{
    [Key]
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public string MappedProp { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

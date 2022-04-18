using System.ComponentModel.DataAnnotations;
using DigitalSkynet.DotnetCore.DataStructures.Interfaces;

namespace DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;

public class DummyEntity : ISoftDeletable, IHasKey<Guid>, ITimestamped
{
    [Key]
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Prop { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

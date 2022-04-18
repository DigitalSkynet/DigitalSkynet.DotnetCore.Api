using AutoMapper;
using DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;

namespace DigitalSkynet.DotnetCore.Tests.Configuration;

public class MapperConfiguration : Profile
{
    public MapperConfiguration()
    {
        CreateMap<DummyEntity, DummyEntityVm>()
            .ForMember(x => x.MappedProp, _ => _.MapFrom(x => x.Prop));

        CreateMap<DummyEntityDto, DummyEntity>();
    }
}
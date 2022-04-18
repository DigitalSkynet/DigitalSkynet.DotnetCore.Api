using DigitalSkynet.DotnetCore.DataAccess.Repository;
using DigitalSkynet.DotnetCore.DataAccess.UnitOfWork;
using DigitalSkynet.DotnetCore.DataStructures.Exceptions.Api;
using DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;
using DigitalSkynet.DotnetCore.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DigitalSkynet.DotnetCore.Tests.DataAccessTests;

public class GenericRepositoryCoreTests : BaseDbTest
{
    private readonly GenericRepository<DummyDbContext, DummyEntity, Guid> _repository;
    private readonly IUnitOfWork _uow;

    private readonly List<DummyEntity> _seededEntities = new();

    public GenericRepositoryCoreTests()
    {
        _repository = ServiceProvider.GetRequiredService<GenericRepository<DummyDbContext, DummyEntity, Guid>>();
        _uow = ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    [Fact]
    public async Task CRUD_ValidDate_Passed()
    {
        var entity = new DummyEntity { Id = new Guid("63e3c92c-73f4-4e7b-93ac-4c02b726a253"), Prop = "Created" };
        _repository.Create(entity);
        await _uow.SaveChangesAsync();
        var entities = _repository.DbContext.ChangeTracker.Entries();
        var result = await _repository.GetByIdAsync(entity.Id);
        Assert.NotNull(result);

        var dto = new DummyEntityDto
        {
            Prop = "Updated"
        };
        await _repository.UpdateAsync(result.Id, dto);
        await _uow.SaveChangesAsync();
        var updated = await _repository.GetByIdAsync(result.Id);
        Assert.NotNull(updated);
        Assert.Equal(dto.Prop, updated.Prop);

        dto.Prop = "Updated in list";

        await _repository.UpdateAsync(new Dictionary<Guid, DummyEntityDto> { { result.Id, dto } });
        await _uow.SaveChangesAsync();
        updated = await _repository.GetByIdAsync(result.Id);
        Assert.NotNull(updated);
        Assert.Equal(dto.Prop, updated.Prop);

        await _repository.DeleteHardAsync(updated.Id);
    }


    [Fact]
    public async Task Delete_NonExitingId_ThrowsApiNotFoundException()
    {
        await Assert.ThrowsAsync<ApiNotFoundException>(() => _repository.DeleteHardAsync(Guid.Empty));
    }
}

using DigitalSkynet.DotnetCore.DataAccess.Enums;
using DigitalSkynet.DotnetCore.DataAccess.Repository;
using DigitalSkynet.DotnetCore.DataAccess.UnitOfWork;
using DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;
using DigitalSkynet.DotnetCore.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DigitalSkynet.DotnetCore.Tests.DataAccessTests;

public class DeletableRepositoryTests : BaseDbTest
{
    protected readonly IGenericDeletableRepository<DummyEntity, Guid> _repository;
    protected readonly IUnitOfWork _uow;

    public DeletableRepositoryTests()
    {
        _uow = ServiceProvider.GetRequiredService<IUnitOfWork>();
        _repository = ServiceProvider.GetRequiredService<IGenericDeletableRepository<DummyEntity, Guid>>();
    }

    #region Read tests

    [Fact]
    public async Task WithDeleted_CheckCount_Two()
    {
        var entity = new DummyEntity { Id = new Guid("63e3c92c-73f4-4e7b-93ac-4c02b726a252"), Prop = "deleted" };
        _repository.Create(entity);
        await _uow.SaveChangesAsync();

        var notDeleted = await _repository.GetByIdAsync(entity.Id);

        await _repository.DeleteAsync(entity.Id);
        await _uow.SaveChangesAsync();

        var withoutIncludeDeleted = await _repository.GetByIdAsync(entity.Id);

        var deleted = await _repository.GetByIdAsync(entity.Id, FetchModes.FindDeleted);

        Assert.NotNull(notDeleted);
        Assert.False(notDeleted.IsDeleted);
        Assert.Equal(entity.Id, notDeleted.Id);
        Assert.Null(withoutIncludeDeleted);
        Assert.NotNull(deleted);
        Assert.Equal(entity.Id, deleted.Id);
        Assert.True(deleted.IsDeleted);
    }

    [Fact]
    public async Task CreateAndSoftlyDelete_ValidData_Passed()
    {
        var dto = new DummyEntityDto
        {
            Prop = "Test"
        };

        var entity = _repository.CreateFromModel(dto);
        await _uow.SaveChangesAsync();
        _repository.Delete(new[] { entity });
        await _uow.SaveChangesAsync();
    }

    #endregion
}

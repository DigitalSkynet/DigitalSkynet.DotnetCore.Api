using DigitalSkynet.DotnetCore.DataAccess.Enums;
using DigitalSkynet.DotnetCore.DataAccess.Repository;
using DigitalSkynet.DotnetCore.DataAccess.UnitOfWork;
using DigitalSkynet.DotnetCore.DataStructures.Enums.Api;
using DigitalSkynet.DotnetCore.DataStructures.Exceptions.Api;
using DigitalSkynet.DotnetCore.DataStructures.Models.Paging;
using DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;
using DigitalSkynet.DotnetCore.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DigitalSkynet.DotnetCore.Tests.DataAccessTests;

public class GenericRepositoryTests : BaseDbTest
{
    private readonly IGenericRepository<DummyEntity, Guid> _repository;
    private readonly IUnitOfWork _uow;

    private readonly List<DummyEntity> _seededEntities = new();

    public GenericRepositoryTests()
    {
        _repository = ServiceProvider.GetRequiredService<IGenericRepository<DummyEntity, Guid>>();
        _uow = ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    #region Reading tests



    [Fact]
    public async Task ExistsAsyncByPredicate_EntityExists_True()
    {
        var result = await _repository.ExistsAsync(x => x.Id == _seededEntities[0].Id);
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsyncById_EntityExists_True()
    {
        var result = await _repository.ExistsAsync(_seededEntities[0].Id);
        Assert.True(result);
    }

    [Fact]
    public async Task GetByIdAsync_EntityExists_GetsSuccessfully()
    {
        var result = await _repository.GetByIdAsync(_seededEntities[0].Id);
        Assert.NotNull(result);
        Assert.Equal(_seededEntities[0].Id, result.Id);
        Assert.Equal(_seededEntities[0].Prop, result.Prop);
    }

    [Fact]
    public async Task GetByIdAsync_EntityDoesNotExist_ReturnsNull()
    {
        var result = await _repository.GetByIdAsync(Guid.NewGuid());
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdsAsync_EntitiesExist_ReturnsEntities()
    {
        var result = await _repository.GetByIdsAsync(_seededEntities.Select(x => x.Id));
        Assert.Collection(result,
            x => Assert.Equal(_seededEntities[0].Id, x.Id),
            x => Assert.Equal(_seededEntities[1].Id, x.Id));
    }

    [Fact]
    public async Task GetByIdAsync_OneOfEntitiesMissing_ThrowsException()
    {
        await Assert.ThrowsAsync<ApiNotFoundException>(() =>
            _repository.GetByIdsAsync(new Guid[] { _seededEntities[0].Id, Guid.NewGuid() }));
    }

    [Fact]
    public async Task FindPagedPaged_PageSizeOneDifferentOrder_OneItemReturnedSortingWorks()
    {
        var sortings = new List<SortModel> {
            new SortModel {
                FieldName = "Prop",
                Direction = SortDirections.Asc
            }
        };

        var pagingModel = new PagingModel
        {
            Filter = "",
            PageNumber = 1,
            PageSize = 1,
            Sort = sortings
        };

        var paged = await _repository.FilterPagedAsync(x => true, pagingModel.PageNumber, pagingModel.PageSize, sortings);
        Assert.NotNull(paged);
        Assert.NotNull(paged.Data);
        Assert.Equal(2, paged.Total);
        Assert.Equal(1, paged.PageNumber);
        Assert.Equal(1, paged.PageSize);
        Assert.Equal(2, paged.TotalPages);
        Assert.Equal("deleted", paged.Data[0].Prop);

        sortings = new List<SortModel> {
            new SortModel {
                FieldName = "Prop",
                Direction = SortDirections.Desc
            }
        };

        paged = await _repository.FilterPagedAsync(x => true, 1, 1, sortings);
        Assert.NotNull(paged);
        Assert.NotNull(paged.Data);
        Assert.Equal(2, paged.Total); // Since it's not deletable repo
        Assert.Equal("not deleted", paged.Data[0].Prop);
    }

    [Fact]
    public async Task ProjectPaged_InvalidSortingPropertyGiven_ThrowsApiNotFoundException()
    {
        var sortings = new SortModel[]
          {
                        new SortModel
                        {
                            FieldName = "NonExistingProp",
                            Direction = SortDirections.Asc
                        }
          };
        await Assert.ThrowsAsync<ApiNotFoundException>(() => _repository.ProjectPagedToAsync<DummyEntityVm>(x => true, 1, 1, sortings));
    }

    [Fact]
    public async Task ProjectPaged_PageSizeOneDifferentOrder_OneItemReturnedSortingWorks()
    {
        var sortings = new SortModel[]
        {
            new SortModel
            {
                FieldName = "MappedProp",
                Direction = SortDirections.Asc
            },
            new SortModel
            {
                FieldName = "IsDeleted",
                Direction = SortDirections.Asc
            },
            new SortModel
            {
                FieldName = "Id",
                Direction = SortDirections.Desc
            }
        };
        var paged = await _repository.ProjectPagedToAsync<DummyEntityVm>(x => true, 1, 1, sortings);
        Assert.NotNull(paged);
        Assert.NotNull(paged.Data);
        Assert.Equal(2, paged.Total);
        Assert.Equal("deleted", paged.Data[0].MappedProp);



        sortings = new SortModel[]
        {
            new SortModel
            {
                FieldName = "MappedProp",
                Direction = SortDirections.Desc
            }
        };
        paged = await _repository.ProjectPagedToAsync<DummyEntityVm>(x => true, 1, 1, sortings);
        Assert.NotNull(paged);
        Assert.NotNull(paged.Data);
        Assert.Equal(2, paged.Total);
        Assert.Equal("not deleted", paged.Data[0].MappedProp);

        paged = await _repository.ProjectPagedByEntityToAsync<DummyEntityVm>(x => true, 1, 1, sortings);
        Assert.NotNull(paged);
        Assert.NotNull(paged.Data);
        Assert.Equal(2, paged.Total);
        Assert.Equal("not deleted", paged.Data[0].MappedProp);
    }

    #endregion

    #region Creation tests

    [Fact]
    public async Task Create_Entity_CreatesSuccessfully()
    {
        var entity = new DummyEntity { Id = new Guid("766020fd-d945-4104-94fd-94037eade2b3"), Prop = "dummy" };
        _repository.Create(new DummyEntity[] { entity });
        await _uow.SaveChangesAsync();

        var allEntities = await DbContext.Set<DummyEntity>().AsNoTracking().ToListAsync();
        Assert.Collection(allEntities,
            x => Assert.Equal("not deleted", x.Prop),
            x => Assert.Equal("deleted", x.Prop),
            x => Assert.Equal("dummy", x.Prop));

        _repository.DeleteHard(new[] { entity });
        await _uow.SaveChangesAsync();
        var afterDeletion = await _repository.GetByIdAsync(entity.Id);
        Assert.Null(afterDeletion);
    }

    [Fact]
    public async Task CreateFromDtoAndProject_DtoGiven_CreatesSuccessfully()
    {
        var dto = new DummyEntityDto { Prop = "dummy" };
        var entity = _repository.CreateFromModel<DummyEntityDto>(dto);
        await _uow.SaveChangesAsync();

        var entityCreatedVm = await _repository.GetProjectionByIdAsync<DummyEntityVm>(entity.Id);

        var allEntities = await DbContext.Set<DummyEntity>().AsNoTracking().ToListAsync();
        Assert.Collection(allEntities,
            x => Assert.Equal("not deleted", x.Prop),
            x => Assert.Equal("deleted", x.Prop),
            x => Assert.Equal("dummy", x.Prop));

        entity.Prop = "Updated";

        _repository.Update(new[] { entity });
        await _uow.SaveChangesAsync();
        var entityUpdate = await _repository.GetProjectionByIdAsync<DummyEntityVm>(entity.Id);

        Assert.NotNull(entityUpdate);
        Assert.Equal(entity.Prop, entityUpdate.MappedProp);
        Assert.True(entityCreatedVm.UpdatedDate < entityUpdate.UpdatedDate);

        await _repository.DeleteHardAsync(entity.Id);
        await _uow.SaveChangesAsync();
        var afterDeletion = await _repository.GetByIdAsync(entity.Id);
        Assert.Null(afterDeletion);
    }

    #endregion

    #region Extension tests

    [Fact]
    public async Task WithNoQuery_CheckIsWithNoTracking_EqualsTrue()
    {
        var entity = new DummyEntity { Id = Guid.NewGuid(), Prop = "dummy" };
        _repository.Create(entity);
        await _uow.SaveChangesAsync();

        var noTracking = await _repository.GetByIdAsync(entity.Id);
        noTracking.Prop = "2";

        var tracking = await _repository.GetByIdAsync(entity.Id, FetchModes.Tracking);
        tracking.Prop = "3";

        await _uow.SaveChangesAsync();

        var fromDb = await _repository.GetByIdAsync(entity.Id);

        Assert.NotEqual(noTracking.Prop, fromDb.Prop);
        Assert.Equal(tracking.Prop, fromDb.Prop);
    }

    #endregion

    protected override async Task SeedAsync()
    {
        _seededEntities.Add(new DummyEntity { Id = new Guid("3e1c0c3d-da2b-47f0-9381-6fa20e02c4f1"), Prop = "not deleted" });
        _seededEntities.Add(new DummyEntity { Id = new Guid("63e3c92c-73f4-4e7b-93ac-4c02b726a252"), Prop = "deleted", IsDeleted = true });
        DbContext.AddRange(_seededEntities);
        await DbContext.SaveChangesAsync();
    }
}

using System.Data;
using DigitalSkynet.DotnetCore.DataAccess.UnitOfWork;
using DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;
using DigitalSkynet.DotnetCore.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DigitalSkynet.DotnetCore.Tests.DataAccessTests;

public class TransactionTests : BaseDbTest
{
    [Fact]
    public async Task EnsureTransaction_TransactionCommitted_DataSaved()
    {
        var unitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();
        Guid entityId;

        using (var transaction = await unitOfWork.EnsureTransactionAsync())
        {
            var entity = new DummyEntity { Id = entityId = Guid.NewGuid(), IsDeleted = false, Prop = "fake" };
            DbContext.Add(entity);
            await unitOfWork.SaveChangesAsync();

            transaction.Commit();
        }

        await DbContext.Set<DummyEntity>().AsNoTracking().SingleAsync(x => x.Id == entityId);
    }

    [Fact]
    public async Task EnsureTransaction_TransactionNotCommitted_DataNotSaved()
    {
        var unitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();
        Guid entityId;

        using (var transaction = await unitOfWork.EnsureTransactionAsync())
        {
            var entity = new DummyEntity { Id = entityId = Guid.NewGuid(), IsDeleted = false, Prop = "fake" };
            DbContext.Add(entity);
            await unitOfWork.SaveChangesAsync();
        }

        Assert.False(await DbContext.Set<DummyEntity>().AnyAsync(x => x.Id == entityId));
    }

    [Fact]
    public async Task EnsureTransaction_NestedTransactions_CommittedOnceAndDataSaved()
    {
        var unitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();
        Guid entityId;

        using (var transaction = await unitOfWork.EnsureTransactionAsync())
        {
            using (var transaction2 = await unitOfWork.EnsureTransactionAsync())
            {
                var entity = new DummyEntity { Id = entityId = Guid.NewGuid(), IsDeleted = false, Prop = "fake" };
                DbContext.Add(entity);
                await unitOfWork.SaveChangesAsync();

                transaction2.Commit();
            }

            transaction.Commit();
        }

        await DbContext.Set<DummyEntity>().AsNoTracking().SingleAsync(x => x.Id == entityId);
    }

    [Fact]
    public async Task EnsureTransaction_NestedTransactions_DifferentLevelsNotAllowed()
    {
        var unitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();

        using (var transaction = await unitOfWork.EnsureTransactionAsync(IsolationLevel.Serializable))
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() => unitOfWork.EnsureTransactionAsync(IsolationLevel.ReadCommitted));
        }
    }

}

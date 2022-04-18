using System.Data;
using DigitalSkynet.DotnetCore.DataAccess.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DigitalSkynet.DotnetCore.DataAccess.UnitOfWork;

public class UnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : DbContext
{
    protected readonly TDbContext _dbContext;
    protected ITransactionHandler _transaction;

    public UnitOfWork(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        _dbContext.OnBeforeSaving();
        return _dbContext.SaveChangesAsync(ct);
    }

    public Task<ITransactionHandler> EnsureTransactionAsync(CancellationToken ct = default)
    {
        return EnsureTransactionAsync(IsolationLevel.Unspecified, ct);
    }

    public async Task<ITransactionHandler> EnsureTransactionAsync(IsolationLevel isolationLevel, CancellationToken ct = default)
    {
        IDbContextTransaction transaction = _dbContext.Database.CurrentTransaction;

        if (transaction == null)
        {
            transaction = await _dbContext.Database.BeginTransactionAsync(isolationLevel, ct);
        }
        else
        {
            var currentIsolationLevel = transaction.GetDbTransaction().IsolationLevel;
            if (isolationLevel != IsolationLevel.Unspecified && isolationLevel != currentIsolationLevel)
            {
                throw new InvalidOperationException($"There is an opened transaction with isolation level {isolationLevel}. The level cannot be changed");
            }
        }

        if (_transaction == null || _transaction.IsDisposed)
        {
            _transaction = new TransactionHandler(transaction);
            return _transaction;
        }

        return new NoopTransactionHandler();
    }
}

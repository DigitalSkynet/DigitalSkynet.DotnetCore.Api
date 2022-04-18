using Microsoft.EntityFrameworkCore.Storage;

namespace DigitalSkynet.DotnetCore.DataAccess.Transactions;

public sealed class TransactionHandler : ITransactionHandler
{
    private readonly IDbContextTransaction _transaction;

    public TransactionHandler(IDbContextTransaction transaction)
    {
        _transaction = transaction
            ?? throw new ArgumentNullException(nameof(transaction));
    }

    public bool IsDisposed { get; private set; }

    public bool IsCommitted { get; private set; }

    /// <summary>
    /// Commits the transaction
    /// </summary>
    public void Commit()
    {
        if (IsDisposed)
        {
            throw new ObjectDisposedException("Transaction has been disposed");
        }

        IsCommitted = true;
        _transaction.Commit();
    }

    public void Dispose()
    {
        if (!IsDisposed)
        {
            if (!IsCommitted)
            {
                _transaction.Rollback();
            }
            IsDisposed = true;
            _transaction.Dispose();
        }
    }
}

namespace DigitalSkynet.DotnetCore.DataAccess.Transactions;

public sealed class NoopTransactionHandler : ITransactionHandler
{
    public bool IsDisposed { get; } = false;

    public bool IsCommitted { get; } = false;

    public void Commit()
    {
        // no-op
    }

    public void Dispose()
    {
        // no-op
    }
}

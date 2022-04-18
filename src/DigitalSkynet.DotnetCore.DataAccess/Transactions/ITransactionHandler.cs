namespace DigitalSkynet.DotnetCore.DataAccess.Transactions;

/// <summary>
/// Interface. Descirbes the transaction handler 
/// </summary>
public interface ITransactionHandler : IDisposable
{
    /// <summary>
    /// Commits the transaction
    /// </summary>
    void Commit();

    /// <summary>
    /// Gets is transaction disposed
    /// </summary>
    /// <value></value>
    bool IsDisposed { get; }

    /// <summary>
    /// Gets is transaction commited
    /// </summary>
    /// <value></value>
    bool IsCommitted { get; }
}

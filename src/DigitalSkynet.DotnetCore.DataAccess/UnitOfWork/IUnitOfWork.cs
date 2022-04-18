using System.Data;
using DigitalSkynet.DotnetCore.DataAccess.Transactions;

namespace DigitalSkynet.DotnetCore.DataAccess.UnitOfWork;

/// <summary>
/// Interface. Descirbes the unit of work pattern. Allows to work with transaction
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves the database changes
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task SaveChangesAsync(CancellationToken ct = default);

    /// <summary>
    /// Ensers that transaction is not created and returns a new transaction
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>New transaction or NoOp</returns>
    Task<ITransactionHandler> EnsureTransactionAsync(CancellationToken ct = default);

    /// <summary>
    /// Ensers that transaction is not created and returns a new transaction with specified isolation level
    /// </summary>
    /// <param name="isolationLevel">Isolation level of transaction</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>New transaction or NoOp</returns>
    Task<ITransactionHandler> EnsureTransactionAsync(IsolationLevel isolationLevel, CancellationToken ct = default);
}

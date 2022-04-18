namespace DigitalSkynet.DotnetCore.DataStructures.Misc;

/// <summary>
/// Class. Represents Disposable Action
/// </summary>
public sealed class DisposableAction : IDisposable
{
    private readonly Action _action;

    /// <summary>
    /// ctor. Initializes the class with the action to be executed
    /// </summary>
    /// <param name="action"></param>
    public DisposableAction(Action action)
    {
        _action = action;
    }

    /// <summary>
    /// Invokes the action and disposes
    /// </summary>
    public void Dispose()
    {
        _action?.Invoke();
    }
}

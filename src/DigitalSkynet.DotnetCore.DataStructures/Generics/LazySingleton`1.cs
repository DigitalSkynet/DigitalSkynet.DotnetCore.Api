using System;
using System.Reflection;
using System.Threading;

namespace DigitalSkynet.DotnetCore.DataStructures.Generics;

/// <summary>
/// Class. Represents the LazyLoading logic for Singleton
/// </summary>
/// <typeparam name="T">Any class which needs to be lazily loaded</typeparam>
public static class LazySingleton<T> where T : class
{
    private static readonly Lazy<T> _instance =
        new Lazy<T>(() => (T)typeof(T).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
            null,
            Array.Empty<Type>(),
            Array.Empty<ParameterModifier>()).Invoke(null), LazyThreadSafetyMode.ExecutionAndPublication);


    /// <summary>
    /// Gets the instance of the Lazy Object
    /// </summary>
    public static T Instance => _instance.Value;
}

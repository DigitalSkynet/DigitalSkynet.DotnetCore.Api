using System;
using System.Reflection;

namespace DigitalSkynet.DotnetCore.DataStructures.Generics;

/// <summary>
/// Class. Represents the singleton object
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : class
{
    private sealed class SingletonCreator<S> where S : class
    {
        public static S CreatorInstance { get; } = (S)typeof(S).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            Array.Empty<Type>(),
            Array.Empty<ParameterModifier>()).Invoke(null);
    }

    /// <summary>
    /// Gets the instance of the singleton
    /// </summary>
    public static T Instance
        => SingletonCreator<T>.CreatorInstance;
}

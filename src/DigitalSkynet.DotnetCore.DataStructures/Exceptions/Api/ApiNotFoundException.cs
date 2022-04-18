using System.Collections.Generic;
using System.Linq;
using DigitalSkynet.DotnetCore.DataStructures.Enums.Api;

namespace DigitalSkynet.DotnetCore.DataStructures.Exceptions.Api;

/// <summary>
/// An exception that should be thrown when a user tries to access a missing resource or request a missing entity.
/// </summary>
public class ApiNotFoundException : ApiException
{
    /// <summary>
    /// Default message template "Cannot find {0} with key(s) {1}"
    /// </summary>
    /// <value></value>
    protected const string DefaultUserMessageFormat = "Cannot find {0} with key(s) {1}";

    /// <summary>
    /// ctor. Initializes the class with given user message
    /// </summary>
    /// <param name="userMessage">The message which will be shown to user</param>
    public ApiNotFoundException(string userMessage)
        : base(userMessage)
    { }

    /// <summary>
    /// ctor. Initializes the class with given entity name and key
    /// </summary>
    /// <param name="entityName">The name of the entity which is not found</param>
    /// <param name="key">The requested id of the entity</param>
    public ApiNotFoundException(string entityName, string key)
        : base(string.Format(DefaultUserMessageFormat, entityName, key))
    {
        EntityName = entityName;
        Keys = new List<string> { key };
    }

    /// <summary>
    /// ctor. Initializes the class with given entity name and multiple requested keys
    /// </summary>
    /// <param name="entityName">The name of the entity which is not found</param>
    /// <param name="keys">The ids requested</param>
    public ApiNotFoundException(string entityName, params string[] keys)
        : this(entityName, keys as IEnumerable<string>)
    {
    }

    /// <summary>
    /// ctor. Initializes the class with given entity name and multiple requested keys
    /// </summary>
    /// <param name="entityName">The name of the entity which is not found</param>
    /// <param name="keys">The ids requested</param>
    public ApiNotFoundException(string entityName, IEnumerable<string> keys)
        : base(string.Format(DefaultUserMessageFormat, entityName, string.Join(", ", keys)))
    {
        Keys = keys.ToList();
    }

    /// <summary>
    /// Gets the response type for the error
    /// </summary>
    public override ResponseTypes ResponseType => ResponseTypes.NotFound;

    /// <summary>
    /// Gets or sets the entity name
    /// </summary>
    /// <value></value>
    public string EntityName { get; set; }

    /// <summary>
    /// Gets or sets the list of requested ids
    /// </summary>
    /// <value></value>
    public List<string> Keys { get; set; }
}

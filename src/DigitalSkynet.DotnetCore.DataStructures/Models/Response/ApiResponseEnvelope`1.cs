using DigitalSkynet.DotnetCore.DataStructures.Enums.Api;

namespace DigitalSkynet.DotnetCore.DataStructures.Models.Response;

/// <summary>
/// Class. Represetns response envelope
/// </summary>
/// <typeparam name="TResponse">The data type to be enveloped</typeparam>
public class ApiResponseEnvelope<TResponse> : ApiResponseEnvelope
{
    /// <summary>
    /// ctor. Initializes the class with the data of the result
    /// </summary>
    /// <param name="result">Result data</param>
    public ApiResponseEnvelope(TResponse result)
        : base(ResponseTypes.Success)
    {
        Data = result;
    }

    /// <summary>
    /// Gets or sets the result data
    /// </summary>
    /// <value></value>
    public TResponse Data { get; set; }
}

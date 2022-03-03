using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace App.API.Services.OpcUa.ApiModels;

/// <summary>
/// The log type.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum LogType
{
    /// <summary>
    /// The info.
    /// </summary>
    [EnumMember(Value = "Info")]
    Info,

    /// <summary>
    /// The warning.
    /// </summary>
    [EnumMember(Value = "Warning")]
    Warning,

    /// <summary>
    /// The error.
    /// </summary>
    [EnumMember(Value = "Error")]
    Error
}

using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace App.API.Services.OpcUa;

/// <summary>
/// The OPC UA running mode.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum OpcUaMode
{
    /// <summary>
    /// The offline.
    /// </summary>
    [EnumMember(Value = "Offline")]
    Offline,

    /// <summary>
    /// The online.
    /// </summary>
    [EnumMember(Value = "Online")]
    Online
}

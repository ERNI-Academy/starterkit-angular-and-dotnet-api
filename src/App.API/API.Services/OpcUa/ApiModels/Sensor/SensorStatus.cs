using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace App.API.Services.OpcUa.ApiModels.Sensor;

/// <summary>
/// The sensor status.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum SensorStatus
{
    /// <summary>
    /// The sensor status is unknown.
    /// </summary>
    [EnumMember(Value = "Unknown")]
    Unknown,

    /// <summary>
    /// The sensor is connected.
    /// </summary>
    [EnumMember(Value = "Connected")]
    Connected,

    /// <summary>
    /// The sensor is failed.
    /// </summary>
    [EnumMember(Value = "Failed")]
    Failed,
}

using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace App.API.Services.OpcUa.ApiModels;

/// <summary>
/// The operation status.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum OperationStatus
{
    /// <summary>
    /// The operation cycle currently not started. Colour: Grey.
    /// </summary>
    [EnumMember(Value = "NotStarted")] 
    NotStarted, 

    /// <summary>
    /// The operation cycle was done successfully. Colour: Green.
    /// </summary>
    [EnumMember(Value = "Done")]
    Done,

    /// <summary>
    /// The operation cycle currently executing. Colour: Green Blinking.
    /// </summary>
    [EnumMember(Value = "InProgress")]
    InProgress,

    /// <summary>
    /// The operation cycle currently stopped. Colour: Yellow.
    /// </summary>
    [EnumMember(Value = "Stopped")]
    Stopped,

    /// <summary>
    /// The operation cycle stopped due to an error. Colour: Red.
    /// </summary>
    [EnumMember(Value = "Failed")]
    Failed
}

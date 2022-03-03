using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace App.API.Services.OpcUa.ApiModels.Module;

/// <summary>
/// The matching status for each module camera.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum FeatureMatchingState
{
    /// <summary>
    /// Feature lost state. Colour: Red.
    /// </summary>
    [EnumMember(Value = "Lost")]
    Lost,

    /// <summary>
    /// Feature with low quality. Colour: Yellow.
    /// </summary>
    [EnumMember(Value = "LowQuality")]
    LowQuality,

    /// <summary>
    /// Feature tracked correctly. Colour: Green.
    /// </summary>
    [EnumMember(Value = "TrackedCorrectly")]
    TrackedCorrectly
}


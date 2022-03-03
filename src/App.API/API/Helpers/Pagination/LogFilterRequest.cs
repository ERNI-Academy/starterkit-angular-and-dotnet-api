using System;
using System.Runtime.Serialization;

using App.API.Services.OpcUa.ApiModels;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace App.API.Helpers.Pagination;

/// <summary>
/// The order.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum Order
{
    /// <summary>
    /// The ascending.
    /// </summary>
    [EnumMember(Value = "Ascending")]
    Ascending,

    /// <summary>
    /// The descending.
    /// </summary>
    [EnumMember(Value = "Descending")]
    Descending,
}

/// <summary>
/// The log list filter.
/// </summary>
public class LogFilterRequest : PageInfo
{
    /// <summary>
    /// Gets or sets the severity. 
    /// Optional, comma-separated multiple severity levels.
    /// </summary>
    public LogType? Type { get; set; }

    /// <summary>
    /// Gets or sets the from.
    /// Optional, starting date
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// Gets or sets the to.
    /// Optional, ending date.
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is acknowledged.
    /// </summary>
    public bool? IsAcknowledged { get; set; }

    /// <summary>
    /// Gets or sets the code.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the module.
    /// </summary>
    public string Module { get; set; }

    /// <summary>
    /// Gets or sets the order.
    /// </summary>
    public Order? Order { get; set; }
}

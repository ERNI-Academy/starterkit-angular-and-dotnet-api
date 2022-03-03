using System;
using System.ComponentModel.DataAnnotations;

namespace App.API.Services.OpcUa.ApiModels;

/// <summary>
/// The log.
/// </summary>
public class Log
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the time.
    /// </summary>
    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    [EnumDataType(typeof(LogType))]
    public LogType Type { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is acknowledged.
    /// </summary>
    public bool IsAcknowledged { get; set; }

    /// <summary>
    /// Gets or sets the code.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the module.
    /// </summary>
    public string Module { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the actions.
    /// </summary>
    public string Actions { get; set; }
}

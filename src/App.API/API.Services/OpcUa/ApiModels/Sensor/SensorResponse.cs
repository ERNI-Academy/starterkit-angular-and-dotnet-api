namespace App.API.Services.OpcUa.ApiModels.Sensor;

/// <summary>
/// The sensor response.
/// </summary>
public class SensorResponse
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public SensorStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the module id.
    /// </summary>
    public int ModuleId { get; set; }
}

namespace App.API.Services.OpcUa.ApiModels.Sensor;

/// <summary>
/// The sensor extr calib response.
/// </summary>
public class ExtrinsicCalibResponse
{
    /// <summary>
    /// Gets or sets the module id.
    /// </summary>
    public int ModuleId { get; set; }

    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public int SensorId { get; set; }

    /// <summary>
    /// Gets or sets the x.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Gets or sets the y.
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// Gets or sets the z.
    /// </summary>
    public float Z { get; set; }

    /// <summary>
    /// Gets or sets the qw.
    /// </summary>
    public float Qw { get; set; }

    /// <summary>
    /// Gets or sets the qx.
    /// </summary>
    public float Qx { get; set; }

    /// <summary>
    /// Gets or sets the qy.
    /// </summary>
    public float Qy { get; set; }

    /// <summary>
    /// Gets or sets the qz.
    /// </summary>
    public float Qz { get; set; }
}

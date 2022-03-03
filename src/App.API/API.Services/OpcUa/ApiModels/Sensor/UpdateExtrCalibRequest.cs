namespace App.API.Services.OpcUa.ApiModels.Sensor;

/// <summary>
/// The update extr calib request.
/// </summary>
public class UpdateExtrCalibRequest
{
    /// <summary>
    /// Gets or sets the x.
    /// </summary>
    public float? X { get; set; }

    /// <summary>
    /// Gets or sets the y.
    /// </summary>
    public float? Y { get; set; }

    /// <summary>
    /// Gets or sets the z.
    /// </summary>
    public float? Z { get; set; }

    /// <summary>
    /// Gets or sets the qw.
    /// </summary>
    public float? Qw { get; set; }

    /// <summary>
    /// Gets or sets the qx.
    /// </summary>
    public float? Qx { get; set; }

    /// <summary>
    /// Gets or sets the qy.
    /// </summary>
    public float? Qy { get; set; }

    /// <summary>
    /// Gets or sets the qz.
    /// </summary>
    public float? Qz { get; set; }
}

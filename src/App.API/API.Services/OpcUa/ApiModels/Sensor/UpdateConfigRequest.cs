namespace App.API.Services.OpcUa.ApiModels.Sensor;

/// <summary>
/// The update config request.
/// </summary>
public class UpdateConfigRequest
{
    /// <summary>
    /// Gets or sets the label.
    /// </summary>
    public string SensorName { get; set; }

    /// <summary>
    /// Gets or sets the serial number.
    /// </summary>
    public string SerialNumber { get; set; }

    /// <summary>
    /// Gets or sets the ip.
    /// </summary>
    public string Ip { get; set; }


    /// <summary>
    /// Gets or sets the fx.
    /// </summary>
    public float? Fx { get; set; }

    /// <summary>
    /// Gets or sets the fy.
    /// </summary>
    public float? Fy { get; set; }

    /// <summary>
    /// Gets or sets the cx.
    /// </summary>
    public float? Cx { get; set; }

    /// <summary>
    /// Gets or sets the cy.
    /// </summary>
    public float? Cy { get; set; }

    /// <summary>
    /// Gets or sets the k 1.
    /// </summary>
    public float? K1 { get; set; }

    /// <summary>
    /// Gets or sets the k 2.
    /// </summary>
    public float? K2 { get; set; }

    /// <summary>
    /// Gets or sets the k 3.
    /// </summary>
    public float? K3 { get; set; }

    /// <summary>
    /// Gets or sets the k 4.
    /// </summary>
    public float? K4 { get; set; }

    /// <summary>
    /// Gets or sets the k 5.
    /// </summary>
    public float? K5 { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    public float? Width { get; set; }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    public float? Height { get; set; }
}

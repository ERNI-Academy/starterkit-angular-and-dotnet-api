namespace App.API.Services.OpcUa.ApiModels.Module;

/// <summary>
/// The hardware status response.
/// </summary>
public class HardwareStatusResponse
{
    /// <summary>
    /// Gets or sets the camera.
    /// </summary>
    public OperationStatus Camera { get; set; }

    /// <summary>
    /// Gets or sets the lights.
    /// </summary>
    public OperationStatus Lights { get; set; }
}

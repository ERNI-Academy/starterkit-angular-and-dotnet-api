namespace App.API.Services.OpcUa.ApiModels.Module;

/// <summary>
/// The process status response.
/// </summary>
public class ProcessStatusResponse
{
    /// <summary>
    /// Gets or sets the number of cycles.
    /// </summary>
    public int NbrCycles { get; set; }

    /// <summary>
    /// Gets or sets the current Step.
    /// </summary>
    public int Step { get; set; }

    /// <summary>
    /// Gets or sets the translation error in mm.
    /// </summary>
    public float TError { get; set; }

    /// <summary>
    /// Gets or sets the rotation error in degrees.
    /// </summary>
    public int RError { get; set; }

    /// <summary>
    /// Gets or sets the matching status for each module camera..
    /// </summary>
    public FeatureMatchingState Matching { get; set; }

}

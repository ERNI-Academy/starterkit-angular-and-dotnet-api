namespace App.API.Services.OpcUa.ApiModels.Module;

/// <summary>
/// The current operation response.
/// </summary>
public class CurrentOperationResponse
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the idle status.
    /// </summary>
    public OperationStatus Idle { get; set; }

    /// <summary>
    /// Gets or sets the ready status.
    /// </summary>
    public OperationStatus Ready { get; set; }

    /// <summary>
    /// Gets or sets the searching status.
    /// </summary>
    public OperationStatus Searching { get; set; }

    /// <summary>
    /// Gets or sets the synching status.
    /// </summary>
    public OperationStatus Synching { get; set; }

    /// <summary>
    /// Gets or sets the linked status.
    /// </summary>
    public OperationStatus Linked { get; set; }

    /// <summary>
    /// Gets or sets the resetting status.
    /// </summary>
    public OperationStatus Resetting { get; set; }
}

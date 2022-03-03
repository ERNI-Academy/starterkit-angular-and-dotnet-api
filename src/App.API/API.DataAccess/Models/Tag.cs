namespace App.API.DataAccess.Models;

#region TagEntityType

/// <summary>
/// The tag.
/// </summary>
public class Tag
{
    /// <summary>
    /// The Id.
    /// </summary>
    private readonly int Id;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tag"/> class.
    /// </summary>
    /// <param name="label">
    /// The label.
    /// </param>
    public Tag(string label) => Label = label;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tag"/> class.
    /// </summary>
    /// <param name="id">
    /// The Id.
    /// </param>
    /// <param name="label">
    /// The label.
    /// </param>
    private Tag(int id, string label)
    {
        Id = id;
        Label = label;
    }

    /// <summary>
    /// Gets the label.
    /// </summary>
    public string Label { get; }

    /// <summary>
    /// Gets or sets the count.
    /// </summary>
    public int Count { get; set; }
}
#endregion

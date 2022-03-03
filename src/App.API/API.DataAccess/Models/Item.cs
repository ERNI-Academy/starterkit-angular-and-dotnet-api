using System.Collections.Generic;
using System.Linq;

namespace App.API.DataAccess.Models;

#region ItemEntityType

/// <summary>
/// The item.
/// </summary>
public class Item
{

    /// <summary>
    /// The Id.
    /// </summary>
    private readonly int Id;

    /// <summary>
    /// The tags.
    /// </summary>
    private readonly List<Tag> tags = new();


    /// <summary>
    /// Initializes a new instance of the <see cref="Item"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    public Item(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Item"/> class.
    /// </summary>
    /// <param name="id">
    /// The Id.
    /// </param>
    /// <param name="name">
    /// The name.
    /// </param>
    private Item(int id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The tags.
    /// </summary>
    public IReadOnlyList<Tag> Tags => tags;


    /// <summary>
    /// The add tag.
    /// </summary>
    /// <param name="label">The label.</param>
    /// <returns>The <see cref="Tag"/>.</returns>
    public Tag AddTag(string label)
    {
        var tag = tags.FirstOrDefault(t => t.Label == label);

        if (tag == null)
        {
            tag = new Tag(label);
            tags.Add(tag);
        }

        tag.Count++;

        return tag;
    }

}
#endregion

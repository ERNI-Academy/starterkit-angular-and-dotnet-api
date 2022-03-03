using System.Collections.Generic;
using System.Linq;

using App.API.DataAccess.Contexts;
using App.API.DataAccess.Models;

using Microsoft.EntityFrameworkCore;

namespace App.API.Services.Items;

/// <summary>
/// The items service.
/// </summary>
public class ItemsService
{
    /// <summary>
    /// The context.
    /// </summary>
    private readonly ApplicationContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemsService"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public ItemsService(ApplicationContext context)
        => this.context = context;

    #region Get

    /// <summary>
    /// The get all.
    /// </summary>
    /// <returns> The list of items.</returns>
    public IEnumerable<Item> GetAll()
        => context.Set<Item>().Include(e => e.Tags).OrderBy(e => e.Name);

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="itemName">The item name.</param>
    /// <returns>The <see cref="Item"/>.</returns>
    public Item Get(string itemName)
        => context.Set<Item>().Include(e => e.Tags).FirstOrDefault(e => e.Name == itemName);
    #endregion

    #region PostItem

    /// <summary>
    /// The create item.
    /// </summary>
    /// <param name="itemName">The item name.</param>
    /// <returns>The <see cref="Item"/>.</returns>
    public Item CreateItem(string itemName)
    {
        var item = context.Add(new Item(itemName)).Entity;

        context.SaveChanges();

        return item;
    }
    #endregion

    #region PostTag

    /// <summary>
    /// The create tag.
    /// </summary>
    /// <param name="itemName">The item name.</param>
    /// <param name="tagLabel">The tag label.</param>
    /// <returns>The <see cref="Tag"/>.</returns>
    public Tag CreateTag(string itemName, string tagLabel)
    {
        var tag = context
            .Set<Item>()
            .Include(e => e.Tags)
            .Single(e => e.Name == itemName)
            .AddTag(tagLabel);

        context.SaveChanges();

        return tag;
    }
    #endregion

    #region DeleteItem

    /// <summary>
    /// The delete item.
    /// </summary>
    /// <param name="itemName">The item name.</param>
    /// <returns>The <see cref="Item"/>.</returns>
    public Item DeleteItem(string itemName)
    {
        var item = context
            .Set<Item>()
            .SingleOrDefault(e => e.Name == itemName);

        if (item == null)
        {
            return item;
        }

        context.Remove(item);
        context.SaveChanges();

        return item;
    }
    #endregion
}

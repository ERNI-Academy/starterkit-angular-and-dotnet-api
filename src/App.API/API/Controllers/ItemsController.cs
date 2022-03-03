using System.Collections.Generic;
using App.API.DataAccess.Models;
using App.API.Services.Items;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// The items controller.
/// </summary>
[Route("items")]
[ApiController]
[AllowAnonymous]
public class ItemsController : ControllerBase
{
    #region Constructor

    /// <summary>
    /// The items service.
    /// </summary>
    private readonly ItemsService itemsService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemsController"/> class.
    /// </summary>
    /// <param name="itemsService">
    /// The items service.
    /// </param>
    public ItemsController(ItemsService itemsService)
    {
        this.itemsService = itemsService;
    }
    #endregion

    #region Get

    /// <summary>
    /// Get the list of the items.
    /// </summary>
    /// <returns>The list of all items.</returns>
    [HttpGet]
    public IEnumerable<Item> Get()
        => itemsService.GetAll();

    /// <summary>
    /// Get a item by id.
    /// </summary>
    /// <param name="itemName">The item name.</param>
    /// <returns>The <see cref="Item"/>.</returns>
    [HttpGet]
    [Route("{itemName}")]
    public Item Get(string itemName)
        => itemsService.Get(itemName);
    #endregion

    #region PostItem

    /// <summary>
    /// Create a new item.
    /// </summary>
    /// <param name="itemName">The item name.</param>
    /// <returns>The item details.</returns>
    [HttpPost]
    [Route("{itemName}")]

    public ActionResult<Item> PostItem(string itemName)
    {
        var item = itemsService.CreateItem(itemName);

        return item;
    }
    #endregion

    #region PostTag

    /// <summary>
    /// Create a new tag.
    /// </summary>
    /// <param name="itemName">The item name.</param>
    /// <param name="tagLabel">The tag label.</param>
    /// <returns>The tag details.</returns>
    [HttpPost]
    [Route("tag/{itemName}/{tagLabel}")]

    public ActionResult<Tag> PostTag(string itemName, string tagLabel)
    {
        var tag = itemsService.CreateTag(itemName, tagLabel);

        return tag;
    }
    #endregion

    #region DeleteItem

    /// <summary>
    /// Delete an item.
    /// </summary>
    /// <param name="itemName">The item name.</param>
    /// <returns>The item deleted.</returns>
    [HttpDelete("{itemName}")]
    public ActionResult<Item> DeleteItem(string itemName)
    {
        var item = itemsService.DeleteItem(itemName);

        if (item == null)
        {
            return NotFound();
        }

        return item;
    }
    #endregion
}

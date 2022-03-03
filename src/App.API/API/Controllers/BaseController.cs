
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// The base controller.
/// </summary>
public class BaseController : ControllerBase
{
    /// <summary>
    /// The requester id.
    /// </summary>
    public int RequesterId => (int)HttpContext.Items["RequesterId"];
}

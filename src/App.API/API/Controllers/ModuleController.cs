using System.Collections.Generic;

using App.API.Helpers;
using App.API.Services.OpcUa.ApiModels.Module;
using App.API.Services.OpcUa.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.API.Controllers;

/// <summary>
/// The module controller.
/// </summary>
[Route("module")]
[Authorize]
[ApiController]
public class ModuleController : BaseController
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<ModuleController> logger;

    /// <summary>
    /// The module service.
    /// </summary>
    private readonly IModuleService moduleService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModuleController"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="moduleService">The module service.</param>
    public ModuleController(
        ILogger<ModuleController> logger,
        IModuleService moduleService)
    {
        this.logger = logger;
        this.moduleService = moduleService;
    }

    /// <summary>
    /// Get all modules available.
    /// </summary>
    /// <returns>The <see cref="IActionResult"/>.</returns>
    /// <response code="200">Returns the all modules available.</response>
    /// <response code="500">An internal error occurred.</response>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<Module>> GetAll()
    {
        logger.LogDebug($"Get all module request received.");
        return Ok(moduleService.GetAll());
    }

    /// <summary>
    /// The get a module by moduleId.
    /// </summary>
    /// <param name="moduleId" example="1">The module identifier.</param>
    /// <returns>The <see cref="IActionResult"/>.</returns>
    /// <response code="200">Returns the data for the specified module.</response>
    /// <response code="404">If the module for the specified id does not exist.</response>  
    /// <response code="500">An internal error occurred.</response>
    [HttpGet("{moduleId:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<Module> GetById(int moduleId)
    {
        logger.LogDebug($"Get module request received. Module moduleId: {moduleId}");
        return Ok(moduleService.GetById(moduleId));
    }

    /// <summary>
    /// Get the current operation estatus for the specified module.
    /// </summary>
    /// <param name="moduleId" example="1">The module moduleId.</param>
    /// <returns>The <see cref="IActionResult"/>.</returns>
    /// <response code="200">Returns the current operation status.</response>
    /// <response code="500">An internal error occurred.</response>
    [HttpGet("{moduleId:int}/status/current-operation")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<CurrentOperationResponse> CurrentOperationStatus(int moduleId)
    {
        logger.LogDebug($"Get current operation status request received. Module moduleId: {moduleId}");
        return Ok(moduleService.CurrentOperation(moduleId));
    }

    /// <summary>
    /// Get process status for the module.
    /// </summary>
    /// <param name="moduleId" example="1">The module identifier.</param>
    /// <returns>The <see cref="IActionResult"/>.</returns>
    /// <response code="200">Returns the process status for the module.</response>
    /// <response code="500">An internal error occurred.</response>
    [HttpGet("{moduleId:int}/status/process")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<ProcessStatusResponse> ProcessStatus(int moduleId)
    {
        logger.LogDebug($"Get process status request received. Module moduleId: {moduleId}");
        return Ok(moduleService.ProcessStatus(moduleId));
    }

    /// <summary>
    /// Get the hardware status.
    /// </summary>
    /// <param name="moduleId" example="1">The module identifier.</param>
    /// <returns>The <see cref="IActionResult"/>.</returns>
    /// <response code="200">Returns the hardware status for the module.</response>
    /// <response code="500">An internal error occurred.</response>
    [HttpGet("{moduleId:int}/status/hardware")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<HardwareStatusResponse> HardwareStatus(int moduleId)
    {
        logger.LogDebug($"Get hardware status request received. Module moduleId: {moduleId}");
        return Ok(moduleService.HardwareStatus(moduleId));
    }
}

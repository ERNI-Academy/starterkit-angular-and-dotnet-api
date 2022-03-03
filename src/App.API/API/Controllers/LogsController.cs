using System.Linq;
using App.API.Helpers;
using App.API.Helpers.Pagination;
using App.API.Services.OpcUa.ApiModels;
using App.API.Services.OpcUa.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.API.Controllers;

/// <summary>
/// The logs controller.
/// </summary>
[Route("logs")]
[Helpers.Authorize]
[ApiController]
public class LogsController : BaseController
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<LogsController> logger;

    /// <summary>
    /// The log service.
    /// </summary>
    private readonly ILogService logService;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogsController"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="logService">The log service.</param>
    public LogsController(
        ILogger<LogsController> logger,
        ILogService logService)
    {
        this.logger = logger;
        this.logService = logService;
    }

    /// <summary>
    /// Get all logs available.
    /// </summary>
    /// <param name="filterRequest">The filterRequest.</param>
    /// <returns>The list of all available logs.</returns>
    /// <response code="200">Returns list of all available logs.</response>
    /// <response code="500">An internal error occurred.</response>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<PagedResultResponse<Log>> GetAll([FromQuery] LogFilterRequest filterRequest)
    {
        filterRequest.Page ??= 1;

        logger.LogDebug("Get all <logs> request received.");


        var logs = logService.GetAll(
            filterRequest.Type,
            filterRequest.Code,
            filterRequest.From,
            filterRequest.To,
            filterRequest.Module,
            filterRequest.IsAcknowledged,
            filterRequest.Order == Order.Ascending);

        var response = logs?.AsQueryable().PagedResult((int)filterRequest.Page, filterRequest.PageSize);
        return Ok(response);
    }

    /// <summary>
    /// Get a log by its identifier.
    /// </summary>
    /// <param name="id" example="1">The log identifier.</param>
    /// <returns>The log details</returns>
    /// <response code="200">The log details.</response>
    /// <response code="404">If the log for the specified id does not exist.</response>  
    /// <response code="500">An internal error occurred.</response>  
    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<Log> GetById(int id)
    {
        logger.LogDebug($"Get <log> with {id} request received.");
        var log = logService.GetById(id);
        return Ok(log);
    }

    /// <summary>
    /// Set the acknowledge status for a log.
    /// </summary>
    /// <param name="id" example="1">The log identifier.</param>
    /// <returns>The status code if the request was successfully processed.</returns>
    /// <response code="202">If the request was successfully processed.</response>
    /// <response code="404">If the log for the specified id does not exist.</response>  
    /// <response code="500">An internal error occurred.</response>  
    [HttpPost("set-ack/{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<Log> SetAck(int id)
    {
        logger.LogDebug($"Setting Akc to <log> with {id} request received.");

        var log = logService.SetAck(id);
        return Accepted(log);
    }

    /// <summary>
    /// Set the acknowledge status for all logs.
    /// </summary>
    /// <returns>The status code if the request was successfully processed.</returns>
    /// <response code="202">If the request was successfully processed.</response>
    /// <response code="500">An internal error occurred.</response>  
    [HttpPost("set-ack-all")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult SetAckAll()
    {
        logger.LogDebug($"Setting Akc for all logs request received.");

        logService.SetAckAll();
        return Accepted();
    }
}

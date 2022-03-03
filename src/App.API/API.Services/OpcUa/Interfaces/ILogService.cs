using System;
using System.Collections.Generic;
using App.API.Services.OpcUa.ApiModels;

namespace App.API.Services.OpcUa.Interfaces;

/// <summary>
/// The LogService interface.
/// </summary>
public interface ILogService
{
    /// <summary>
    /// The get all logs filtered by the parameters.
    /// </summary>
    /// <param name="type">The type toDate filter.</param>
    /// <param name="code">The code toDate filter.</param>
    /// <param name="fromDate">The fromDate date toDate filter.</param>
    /// <param name="toDate">The toDate date toDate filter.</param>
    /// <param name="module">The module toDate filter.</param>
    /// <param name="isAcknowledged">The is acknowledged toDate filter.</param>
    /// <param name="orderAscending">The order of the elements</param>
    /// <returns>The list of the logs available.</returns>
    IEnumerable<Log> GetAll(
        LogType? type,
        string code,
        DateTime? fromDate,
        DateTime? toDate,
        string module,
        bool? isAcknowledged,
        bool? orderAscending);

    /// <summary>
    /// The get by id.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="Log"/>.</returns>
    Log GetById(int id);

    /// <summary>
    /// The set ack.
    /// </summary>
    /// <param name="id">The log identifier.</param>
    /// <returns>The <see cref="Log"/>.</returns>
    Log SetAck(int id);

    /// <summary>
    /// The set ack all.
    /// </summary>
    void SetAckAll();
}

using System;
using System.Collections.Generic;
using App.API.Services.OpcUa.ApiModels;
using App.API.Services.OpcUa.Interfaces;

namespace App.API.Services.OpcUa.OnlineServices;

/// <summary>
/// The log online service.
/// </summary>
public class LogOnlineService : ILogService
{
    /// <inheritdoc/>
    public IEnumerable<Log> GetAll(
        LogType? type,
        string code,
        DateTime? fromDate,
        DateTime? toDate,
        string module,
        bool? filterRequestIsAcknowledged,
        bool? orderAscending)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    Log ILogService.GetById(int id)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public Log SetAck(int id)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void SetAckAll()
    {
        throw new System.NotImplementedException();
    }
}

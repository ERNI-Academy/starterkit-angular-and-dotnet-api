using System;
using System.Collections.Generic;
using App.API.Services.OpcUa.ApiModels.Module;
using App.API.Services.OpcUa.Interfaces;

namespace App.API.Services.OpcUa.OnlineServices;

/// <summary>
/// The module online service.
/// </summary>
public class ModuleOnlineService : IModuleService
{
    /// <inheritdoc/>
    public IEnumerable<Module> GetAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Module GetById(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public CurrentOperationResponse CurrentOperation(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public ProcessStatusResponse ProcessStatus(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public HardwareStatusResponse HardwareStatus(int id)
    {
        throw new NotImplementedException();
    }
}

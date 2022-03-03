using System;
using System.Collections.Generic;
using System.Linq;

using App.API.Services.OpcUa.ApiModels;
using App.API.Services.OpcUa.ApiModels.Module;
using App.API.Services.OpcUa.Interfaces;

using Microsoft.Extensions.Logging;

namespace App.API.Services.OpcUa.OfflineServices;

/// <summary>
/// The module offline service.
/// </summary>
public class ModuleOfflineService : IModuleService
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<ModuleOfflineService> logger;

    /// <summary>
    /// The modulesList.
    /// </summary>
    private IEnumerable<Module> modulesList;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModuleOfflineService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public ModuleOfflineService(ILogger<ModuleOfflineService> logger)
    {
        this.logger = logger;
        SeekFakeData();
    }
    
    /// <inheritdoc/>
    public IEnumerable<Module> GetAll()
    {
        return modulesList;
    }

    /// <inheritdoc/>
    public Module GetById(int id)
    {
        var module = modulesList.FirstOrDefault(m => m.Id == id);
        if (module != null)
        {
            return module;
        }

        logger.LogDebug($"Could not find the log with Id: {id}");
        throw new KeyNotFoundException("Log not found");
    }


    /// <inheritdoc/>
    public CurrentOperationResponse CurrentOperation(int id)
    {
        var rnd = new Random();
        var values = Enum.GetValues(typeof(OperationStatus));
        return new CurrentOperationResponse
        {
            Name = "Moving Robot...",
            Idle = OperationStatus.Done,
            Ready = OperationStatus.Done,
            Searching = (OperationStatus)values.GetValue( rnd.Next((int)OperationStatus.InProgress, values.Length)),
            Synching = OperationStatus.NotStarted,
            Linked = OperationStatus.NotStarted,
            Resetting = OperationStatus.NotStarted,
        };
    }

    /// <inheritdoc/>
    public ProcessStatusResponse ProcessStatus(int id)
    {
        var rnd = new Random();

        var matchingState = Enum.GetValues(typeof(FeatureMatchingState));
        return new ProcessStatusResponse
        {
            NbrCycles = rnd.Next(1, 100),
            Step = rnd.Next(1, 10),
            TError = rnd.Next(0, 1),
            RError = rnd.Next(0, 10),
            Matching = (FeatureMatchingState)matchingState.GetValue(rnd.Next(matchingState.Length)),
        };
    }


    /// <inheritdoc/>
    public HardwareStatusResponse HardwareStatus(int id)
    {
        var rnd = new Random();
        var values = Enum.GetValues(typeof(OperationStatus));
        return new HardwareStatusResponse
        {
            Camera = (OperationStatus)values.GetValue(rnd.Next(values.Length)),
            Lights = (OperationStatus)values.GetValue(rnd.Next(values.Length)),
        };
    }

    /// <summary>
    /// The seek fake data.
    /// </summary>
    private void SeekFakeData()
    {
        modulesList = new List<Module>
                               {
                                   new Module { Id = 1, Name = "UVT" },
                                   new Module { Id = 2, Name = "CVG" }
                               };
    }
}

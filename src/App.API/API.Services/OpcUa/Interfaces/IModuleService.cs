using System.Collections.Generic;

using App.API.Services.OpcUa.ApiModels.Module;

namespace App.API.Services.OpcUa.Interfaces;

/// <summary>
/// The ModuleService interface.
/// </summary>
public interface IModuleService
{
    /// <summary>
    /// The get all modules.
    /// </summary>
    /// <returns>The enumerated modules availables.</returns>
    IEnumerable<Module> GetAll();

    /// <summary>
    /// The get by id.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="Module"/>.</returns>
    Module GetById(int id);

    /// <summary>
    /// The current operation.
    /// </summary>
    /// <param name="id">The id. </param>
    /// <returns>The <see cref="CurrentOperationResponse"/>.</returns>
    CurrentOperationResponse CurrentOperation(int id);

    /// <summary>
    /// The process status.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="ProcessStatusResponse"/>.</returns>
    ProcessStatusResponse ProcessStatus(int id);

    /// <summary>
    /// The hardware status.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="HardwareStatusResponse"/>.</returns>
    HardwareStatusResponse HardwareStatus(int id);
}

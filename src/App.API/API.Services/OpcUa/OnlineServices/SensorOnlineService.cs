using System.Collections.Generic;

using App.API.Services.OpcUa.ApiModels.Sensor;
using App.API.Services.OpcUa.Interfaces;

namespace App.API.Services.OpcUa.OnlineServices;

/// <summary>
/// The sensor online service.
/// </summary>
public class SensorOnlineService : ISensorService
{
    /// <inheritdoc/>
    public IEnumerable<SensorResponse> GetAllByModule(int moduleId)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public SensorResponse GetById(int sensorId)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public SensorResponse Add(int moduleId, string sensorName)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void Delete(int id)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public ConfigResponse Config(int id)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void Config(int id, UpdateConfigRequest request)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public ExtrinsicCalibResponse ExtrinsicCalibration(int id)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void ExtrinsicCalibration(int id, UpdateExtrCalibRequest request)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public ReferenceResponse Reference(int id)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void Reference(int id, UpdateReferenceRequest request)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void ClearReference(int id)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public bool CheckCalibration(int id)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public bool RecalculateCalib(int id)
    {
        throw new System.NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;

using App.API.Services.OpcUa.ApiModels.Sensor;

namespace App.API.Services.OpcUa.Interfaces;

/// <summary>
/// The SensorService interface.
/// </summary>
public interface ISensorService
{
    /// <summary>
    /// The get all by module.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <returns>The <see cref="IEnumerable"/>.</returns>
    IEnumerable<SensorResponse> GetAllByModule(int moduleId);

    /// <summary>
    /// The get by id.
    /// </summary>
    /// <param name="sensorId">The sensor id.</param>
    /// <returns>The <see cref="SensorResponse"/>.</returns>
    SensorResponse GetById(int sensorId);

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="sensorName">The name.</param>
    /// <returns>
    /// The <see cref="SensorResponse"/>.
    /// </returns>
    SensorResponse Add(int moduleId, string sensorName);

    /// <summary>
    /// The delete.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    void Delete(int id);

    /// <summary>
    /// The config.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="ConfigResponse"/>.</returns>
    ConfigResponse Config(int id);

    /// <summary>
    /// The config.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="request">The request.</param>
    void Config(int id, UpdateConfigRequest request);

    /// <summary>
    /// The extrinsic calibration.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="ExtrinsicCalibResponse"/>. </returns>
    ExtrinsicCalibResponse ExtrinsicCalibration(int id);

    /// <summary>
    /// The extrinsic calibration.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="request">The request.</param>
    void ExtrinsicCalibration(int id, UpdateExtrCalibRequest request);

    /// <summary>
    /// The reference.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="ReferenceResponse"/>.</returns>
    ReferenceResponse Reference(int id);

    /// <summary>
    /// The reference.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="request">The request.</param>
    void Reference(int id, UpdateReferenceRequest request);

    /// <summary>
    /// The clear reference.
    /// </summary>
    /// <param name="id">The id.</param>
    void ClearReference(int id);

    /// <summary>
    /// The check calibration.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    bool CheckCalibration(int id);

    /// <summary>
    /// The recalculate calib.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    bool RecalculateCalib(int id);
}


using System;
using System.Collections.Generic;
using System.Linq;
using App.API.Services.OpcUa.ApiModels.Sensor;
using App.API.Services.OpcUa.Interfaces;

using Microsoft.Extensions.Logging;

namespace App.API.Services.OpcUa.OfflineServices;

/// <summary>
/// The sensor offline service.
/// </summary>
public class SensorOfflineService : ISensorService
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<SensorOfflineService> logger;

    /// <summary>
    /// The sensor list.
    /// </summary>
    private IList<SensorResponse> sensorList;

    /// <summary>
    /// The config list.
    /// </summary>
    private List<ConfigResponse> configList;

    /// <summary>
    /// The extrinsic calib list.
    /// </summary>
    private List<ExtrinsicCalibResponse> extrinsicCalibList;

    /// <summary>
    /// The reference list.
    /// </summary>
    private List<ReferenceResponse> referenceList;

    /// <summary>
    /// Initializes a new instance of the <see cref="SensorOfflineService"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    public SensorOfflineService(ILogger<SensorOfflineService> logger)
    {
        this.logger = logger;
        SeedFakeData();
    }

    /// <inheritdoc/>
    public IEnumerable<SensorResponse> GetAllByModule(int moduleId)
    {
        logger.LogDebug($"Get sensors for module with id {moduleId}");
        return sensorList.Where(s => s.ModuleId == moduleId);
    }

    /// <inheritdoc/>
    public SensorResponse GetById(int sensorId)
    {
        return sensorList.FirstOrDefault(s => s.Id == sensorId);
    }

    /// <inheritdoc/>
    public SensorResponse Add(int moduleId, string sensorName)
    {
        var nextId = sensorList.Count + 1;
        var newSensor = new SensorResponse { Id = nextId, ModuleId = moduleId, Name = sensorName, Status = SensorStatus.Unknown };
        sensorList.Add(newSensor);
        configList.Add(new ConfigResponse { ModuleId = moduleId, SensorId = nextId, SensorName = sensorName });
        extrinsicCalibList.Add(new ExtrinsicCalibResponse { ModuleId = moduleId, SensorId = nextId, });
        referenceList.Add(new ReferenceResponse { ModuleId = moduleId, SensorId = nextId, });
        return newSensor;
    }

    /// <inheritdoc/>
    public void Delete(int id)
    {
        var sensor = sensorList.FirstOrDefault(s => s.Id == id);
        if (sensor == null)
        {
            throw new KeyNotFoundException($"Could not find the sensor with id {id}");
        }

        sensorList.Remove(sensor);
        configList.Remove(configList.FirstOrDefault(s => s.SensorId == id));
        extrinsicCalibList.Remove(extrinsicCalibList.FirstOrDefault(s => s.SensorId == id));
        referenceList.Remove(referenceList.FirstOrDefault(s => s.SensorId == id));
    }

    /// <inheritdoc/>
    public ConfigResponse Config(int id)
    {
        return GetConfig(id);
    }

    /// <inheritdoc/>
    public void Config(int id, UpdateConfigRequest request)
    {
        var config = GetConfig(id);

        config.SensorName = request.SensorName != null && config.SensorName != request.SensorName ? request.SensorName : config.SensorName;
        config.SerialNumber = request.SerialNumber != null && config.SerialNumber != request.SerialNumber ? request.SerialNumber : config.SerialNumber;
        config.Ip = request.Ip != null && config.Ip != request.Ip ? request.Ip : config.Ip;
        config.Fx = request.Fx != null && !config.Fx.Equals(request.Fx) ? (float)request.Fx : config.Fx;
        config.Fy = request.Fy != null && !config.Fy.Equals(request.Fy) ? (float)request.Fy : config.Fy;
        config.Cx = request.Cx != null && !config.Cx.Equals(request.Cx) ? (float)request.Cx : config.Cx;
        config.Cy = request.Cy != null && !config.Cy.Equals(request.Cy) ? (float)request.Cy : config.Cy;
        config.K1 = request.K1 != null && !config.K1.Equals(request.K1) ? (float)request.K1 : config.K1;
        config.K2 = request.K2 != null && !config.K2.Equals(request.K2) ? (float)request.K2 : config.K2;
        config.K3 = request.K3 != null && !config.K3.Equals(request.K3) ? (float)request.K3 : config.K3;
        config.K4 = request.K4 != null && !config.K4.Equals(request.K4) ? (float)request.K4 : config.K4;
        config.K5 = request.K5 != null && !config.K5.Equals(request.K5) ? (float)request.K5 : config.K5;
        config.Height = request.Height != null && !config.Height.Equals(request.Height) ? (float)request.Height : config.Height;
        config.Width = request.Width != null && !config.Width.Equals(request.Width) ? (float)request.Width : config.Width;
    }

    /// <inheritdoc/>
    public ExtrinsicCalibResponse ExtrinsicCalibration(int id)
    {
        return GetExtrinsicCalib(id);
    }

    /// <inheritdoc/>
    public void ExtrinsicCalibration(int id, UpdateExtrCalibRequest request)
    {
        var extrinsicCalib = GetExtrinsicCalib(id);

        extrinsicCalib.X = request.X != null && !extrinsicCalib.X.Equals(request.X) ? (float)request.X : extrinsicCalib.X;
        extrinsicCalib.Y = request.Y != null && !extrinsicCalib.Y.Equals(request.Y) ? (float)request.Y : extrinsicCalib.Y;
        extrinsicCalib.Z = request.Z != null && !extrinsicCalib.Z.Equals(request.Z) ? (float)request.Z : extrinsicCalib.Z;
        extrinsicCalib.Qw = request.Qw != null && !extrinsicCalib.Qw.Equals(request.Qw) ? (float)request.Qw : extrinsicCalib.Qw;
        extrinsicCalib.Qx = request.Qx != null && !extrinsicCalib.Qx.Equals(request.Qx) ? (float)request.Qx : extrinsicCalib.Qx;
        extrinsicCalib.Qy = request.Qy != null && !extrinsicCalib.Qy.Equals(request.Qy) ? (float)request.Qy : extrinsicCalib.Qy;
        extrinsicCalib.Qz = request.Qz != null && !extrinsicCalib.Qz.Equals(request.Qz) ? (float)request.Qz : extrinsicCalib.Qz;
    }

    /// <inheritdoc/>
    public ReferenceResponse Reference(int id)
    {
        return GetReference(id);
    }

    /// <inheritdoc/>
    public void Reference(int id, UpdateReferenceRequest request)
    {
        var reference = GetReference(id);

        reference.ExposureTime = request.ExposureTime != null && reference.ExposureTime != request.ExposureTime ? request.ExposureTime : reference.ExposureTime;
        reference.ChessboardSizeHigh = request.ChessboardSizeHigh != null && reference.ChessboardSizeHigh != request.ChessboardSizeHigh ? request.ChessboardSizeHigh : reference.ChessboardSizeHigh;
        reference.ChessboardSizeWidth = request.ChessboardSizeWidth != null && reference.ChessboardSizeWidth != request.ChessboardSizeWidth ? request.ChessboardSizeWidth : reference.ChessboardSizeWidth;
        reference.SquareSize = request.SquareSize != null && !reference.SquareSize.Equals(request.SquareSize) ? request.SquareSize : reference.SquareSize;
    }

    /// <inheritdoc/>
    public void ClearReference(int id)
    {
        var reference = referenceList.FirstOrDefault(sc => sc.SensorId == id);
        if (reference == null)
        {
            throw new KeyNotFoundException($"Could not find the sensor with id {id}");
        }

        reference.ExposureTime = null;
        reference.ChessboardSizeHigh = null;
        reference.ChessboardSizeWidth = null;
        reference.SquareSize = null;
    }

    /// <inheritdoc/>
    public bool CheckCalibration(int id)
    {
        return RandomBool();
    }

    /// <inheritdoc/>
    public bool RecalculateCalib(int id)
    {
        return RandomBool();
    }

    /// <summary>
    /// The random bool.
    /// </summary>
    /// <returns>The <see cref="bool"/>.</returns>
    private static bool RandomBool()
    {
        var rnd = new Random();
        return rnd.Next(100) <= 40;
    }

    /// <summary>
    /// The get config.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="ConfigResponse"/>.</returns>
    private ConfigResponse GetConfig(int id)
    {
        var config = configList.FirstOrDefault(sc => sc.SensorId == id);
        if (config == null)
        {
            throw new KeyNotFoundException($"Could not find the sensor with id {id}");
        }

        return config;
    }

    /// <summary>
    /// The get extrinsic calib.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="ExtrinsicCalibResponse"/>.</returns>
    private ExtrinsicCalibResponse GetExtrinsicCalib(int id)
    {
        var extrinsicCalib = extrinsicCalibList.FirstOrDefault(sc => sc.SensorId == id);
        if (extrinsicCalib == null)
        {
            throw new KeyNotFoundException($"Could not find the sensor with id {id}");
        }

        return extrinsicCalib;
    }

    /// <summary>
    /// The get reference.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="ReferenceResponse"/>.</returns>
    private ReferenceResponse GetReference(int id)
    {
        var reference = referenceList.FirstOrDefault(sc => sc.SensorId == id);
        if (reference == null)
        {
            throw new KeyNotFoundException($"Could not find the sensor with id {id}");
        }

        return reference;
    }

    /// <summary>
    /// The seed fake data.
    /// </summary>
    private void SeedFakeData()
    {
        sensorList = new List<SensorResponse>
                              {
                                  new SensorResponse { Id = 1, Name = "Front Door Cam 1", Status = SensorStatus.Connected, ModuleId = 1 },
                                  new SensorResponse { Id = 2, Name = "Front Door Cam 2", Status = SensorStatus.Unknown, ModuleId = 1 },
                                  new SensorResponse { Id = 3, Name = "Rear Door cam 1", Status = SensorStatus.Connected, ModuleId = 1 },
                                  new SensorResponse { Id = 4, Name = "Camera 1", Status = SensorStatus.Connected, ModuleId = 2 },
                                  new SensorResponse { Id = 5, Name = "Camera 2", Status = SensorStatus.Failed, ModuleId = 2 },
                              };

        configList = new List<ConfigResponse>();
        extrinsicCalibList = new List<ExtrinsicCalibResponse>();
        referenceList = new List<ReferenceResponse>();

        var rnd = new Random();

        foreach (var sensor in sensorList)
        {
            var configSensor = new ConfigResponse
            {
                ModuleId = sensor.ModuleId,
                SensorId = sensor.Id,
                SensorName = sensor.Name,
                SerialNumber = rnd.Next().ToString(),
                Fx = (float)rnd.NextDouble(),
                Fy = (float)rnd.NextDouble(),
                Cx = (float)rnd.NextDouble(),
                Cy = (float)rnd.NextDouble(),
                K1 = (float)rnd.NextDouble(),
                K2 = (float)rnd.NextDouble(),
                K3 = (float)rnd.NextDouble(),
                K4 = (float)rnd.NextDouble(),
                K5 = (float)rnd.NextDouble(),
                Width = (float)rnd.NextDouble(),
                Height = (float)rnd.NextDouble()
            };
            configList.Add(configSensor);

            var extrinsicCalib = new ExtrinsicCalibResponse
            {
                ModuleId = sensor.ModuleId,
                SensorId = sensor.Id,
                X = (float)rnd.NextDouble(),
                Y = (float)rnd.NextDouble(),
                Z = (float)rnd.NextDouble(),
                Qw = (float)rnd.NextDouble(),
                Qx = (float)rnd.NextDouble(),
                Qy = (float)rnd.NextDouble(),
                Qz = (float)rnd.NextDouble(),
            };
            extrinsicCalibList.Add(extrinsicCalib);

            var reference = new ReferenceResponse
            {
                ModuleId = sensor.ModuleId,
                SensorId = sensor.Id,
                ExposureTime = rnd.Next(7000),
                ChessboardSizeHigh = rnd.Next(10),
                ChessboardSizeWidth = rnd.Next(10),
                SquareSize = (float)rnd.NextDouble()
            };
            referenceList.Add(reference);
        }
    }
}

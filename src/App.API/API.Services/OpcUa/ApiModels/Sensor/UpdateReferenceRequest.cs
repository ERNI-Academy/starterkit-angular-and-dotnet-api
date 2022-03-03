namespace App.API.Services.OpcUa.ApiModels.Sensor;

/// <summary>
/// The update reference request.
/// </summary>
public class UpdateReferenceRequest
{
    /// <summary>
    /// Gets or sets the exposure time.
    /// </summary>
    public int? ExposureTime { get; set; }

    /// <summary>
    /// Gets or sets the chessboard size high.
    /// </summary>
    public int? ChessboardSizeHigh { get; set; }

    /// <summary>
    /// Gets or sets the chessboard size width.
    /// </summary>
    public int? ChessboardSizeWidth { get; set; }

    /// <summary>
    /// Gets or sets the square size.
    /// </summary>
    public float? SquareSize { get; set; }
}

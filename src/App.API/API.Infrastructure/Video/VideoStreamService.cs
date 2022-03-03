using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.API.Infrastructure.Video;

/// <summary>
/// The video stream service.
/// </summary>
public class VideoStreamService : IVideoStreamService
{
    /// <summary>
    /// The client.
    /// </summary>
    private readonly HttpClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="VideoStreamService"/> class.
    /// </summary>
    public VideoStreamService()
    {
        client = new HttpClient();
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="VideoStreamService"/> class. 
    /// </summary>
    ~VideoStreamService()
    {
        client?.Dispose();
    }

    /// <inheritdoc/>
    public async Task<Stream> GetVideoByName(string name)
    {
        string urlBlob = name switch
        {
            "earth" => "https://anthonygiretti.blob.core.windows.net/videos/earth.mp4",
            "nature1" => "https://anthonygiretti.blob.core.windows.net/videos/nature1.mp4",
            _ => "https://anthonygiretti.blob.core.windows.net/videos/nature2.mp4",
        };
        return await client.GetStreamAsync(urlBlob);
    }
}

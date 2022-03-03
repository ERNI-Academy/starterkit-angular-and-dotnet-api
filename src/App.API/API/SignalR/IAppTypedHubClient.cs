using System.Threading.Tasks;

namespace App.API.SignalR;

/// <summary>
/// The AppTypedHubClient interface.
/// </summary>
public interface IAppTypedHubClient
{
    /// <summary>
    /// The broadcast message.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="payload">The payload.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    Task BroadcastMessage(string type, string payload);
}

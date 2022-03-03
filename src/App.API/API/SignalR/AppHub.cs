using Microsoft.AspNetCore.SignalR;

namespace App.API.SignalR;

/// <summary>
/// The App hub.
/// </summary>
public class AppHub : Hub<IAppTypedHubClient>
{
}

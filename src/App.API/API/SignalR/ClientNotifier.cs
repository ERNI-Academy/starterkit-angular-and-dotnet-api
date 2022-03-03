using Api.API.Infrastructure.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace App.API.SignalR;

/// <summary>
/// The signal r client notifier.
/// </summary>
public class SignalRClientNotifier : INotifyClient
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<SignalRClientNotifier> logger;

    /// <summary>
    /// The hub context.
    /// </summary>
    private readonly IHubContext<AppHub, IAppTypedHubClient> hubContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SignalRClientNotifier"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="hubContext">The hub context. </param>
    public SignalRClientNotifier(
        ILogger<SignalRClientNotifier> logger,
        IHubContext<AppHub, IAppTypedHubClient> hubContext)
    {
        this.logger = logger;
        this.hubContext = hubContext;
    }

    /// <summary>
    /// The notify client.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="payload">The payload.</param>
    public void NotifyAllClients(NotificationType type, object payload)
    {
        logger.LogInformation($"Notifying changes to all clients. Type:{type} Payload: {payload}");

        string stringPayload = PayloadToString(payload);

        hubContext.Clients.All.BroadcastMessage(type.ToString(), stringPayload);
    }

    private static string PayloadToString(object payload)
    {
        string stringPayload;
        if (payload is string payloadString)
        {
            stringPayload = payloadString;
        }
        else
        {
            stringPayload = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    // serialize in Camel format: the same way that the Controller Actions "deserialize" the objects and Swagger also camel case
                    // the object properties. This way we can use the same type of "objects" (with camelcase properties) in the UI
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
        }

        return stringPayload;
    }

    public void NotifyCurrentClient(string connectionId, NotificationType type, object payload)
    {
        logger.LogInformation($"Notifying changes to single client. Type:{type} Payload: {payload}");

        string stringPayload = PayloadToString(payload);

        hubContext.Clients.Client(connectionId).BroadcastMessage(type.ToString(), stringPayload);
    }
}

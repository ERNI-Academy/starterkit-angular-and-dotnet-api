using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Api.API.Infrastructure.Notifications;

/// <summary>
/// The NotifyClient interface.
/// </summary>
public interface INotifyClient
{
    /// <summary>
    /// The notify client.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="payload">The payload.</param>
    void NotifyAllClients(NotificationType type, object payload);

    /// <summary>
    /// The notify client.
    /// </summary>
    /// <param name="connectionId">SignalR connectionId</param>
    /// <param name="type">The type.</param>
    /// <param name="payload">The payload.</param>
    void NotifyCurrentClient(string connectionId, NotificationType type, object payload);
}

[JsonConverter(typeof(StringEnumConverter))]
public enum NotificationType
{
    /// <summary>
    /// Ascending
    /// </summary>
    [EnumMember(Value = "Logs")]
    Logs,

    /// <summary>
    /// The descending.
    /// </summary>
    [EnumMember(Value = "ModuleStatus")]
    ModuleStatus,
}

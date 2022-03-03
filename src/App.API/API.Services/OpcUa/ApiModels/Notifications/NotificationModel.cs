using Api.API.Infrastructure.Notifications;

namespace App.API.Services.OpcUa.ApiModels.Notifications;

public class NotificationModels
{
    public NotificationModel NotificationModel { get; set; }
    public Log NotificationLogPayload { get; set; }
    // public CurrentOperationResponse NotificationOperationStatusPayload { get; set; }
}

public class NotificationModel
{
    public NotificationType Type { get; set; }
}

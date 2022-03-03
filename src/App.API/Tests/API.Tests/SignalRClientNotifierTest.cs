using System;
using Api.API.Infrastructure.Notifications;
using App.API.Services.OpcUa.ApiModels;
using App.API.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

using Moq;
using Xunit;

namespace App.API.Tests;

/// <summary>
/// The signal r client notifier test.
/// </summary>
public class SignalRClientNotifierTest
{
    /// <summary>
    /// The hub context mock.
    /// </summary>
    private readonly Mock<IHubContext<AppHub, IAppTypedHubClient>> hubContextMock;

    /// <summary>
    /// The testee.
    /// </summary>
    private readonly SignalRClientNotifier testee;

    /// <summary>
    /// Initializes a new instance of the <see cref="SignalRClientNotifierTest"/> class.
    /// </summary>
    public SignalRClientNotifierTest()
    {
        hubContextMock = new Mock<IHubContext<AppHub, IAppTypedHubClient>>();
        var loggerMock = new Mock<ILogger<SignalRClientNotifier>>();
        testee = new SignalRClientNotifier(loggerMock.Object, hubContextMock.Object);
    }

    /// <summary>
    /// The notify client.
    /// </summary>
    [Fact]
    public void NotifyClient()
    {
        // Arrange
        const NotificationType Type = NotificationType.Logs;
        var payload = new Log
                          {
                              Id = 100,
                              Code = "Code Test",
                              IsAcknowledged = true,
                              Module = "Module Test",
                              Title = "Lore Ipsum Title {Code}",
                              Date = DateTime.UtcNow,
                              Description = "Lore Ipsum Description Test",
                              Actions = "Lore Ipsum Actions Test"
                          };

        hubContextMock.Setup(x => x.Clients.All.BroadcastMessage(It.IsAny<string>(), It.IsAny<string>()));

        // Act
        testee.NotifyAllClients(Type, payload);

        // Assert
        hubContextMock.Verify(x => x.Clients.All.BroadcastMessage(Type.ToString(), It.IsAny<string>()));
    }
}

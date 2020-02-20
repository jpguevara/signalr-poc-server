
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace signalr_server.Hubs
{
  public static class UserHandler
  {
    public static HashSet<string> ConnectedIds = new HashSet<string>();
  }

  public class NotificationHub : Hub
  {

    public override Task OnConnectedAsync()
    {
      UserHandler.ConnectedIds.Add(Context.ConnectionId);
      this.SendClients();
      return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
      UserHandler.ConnectedIds.Remove(Context.ConnectionId);
      this.SendClients();
      return base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string user, string message)
    {
      var notification = new Notification
      {
        To = user,
        From = Context.ConnectionId,
        Message = message,
        Status = 1
      };

      await Clients.Client(notification.To).SendAsync("ReceiveMessage", notification);
    }
    public async Task SendClients()
    {
      await Clients.All.SendAsync("ReceiveClients", UserHandler.ConnectedIds);
    }
  }
}
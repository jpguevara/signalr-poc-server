namespace signalr_server.Hubs
{

  public class Notification
  {
    public string Message { get; set; }
    public int Status { get; set; }
    public string To { get; internal set; }
    public string From { get; internal set; }
  }
}
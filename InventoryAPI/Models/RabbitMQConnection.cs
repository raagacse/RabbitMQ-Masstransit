namespace InventoryAPI.Models;

public class RabbitMQConnection
{
    public string hostName { get; set; } = string.Empty;
    public string userName { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public string virtualHost { get; set; } = string.Empty;
    public string automaticRecoveryEnabled { get; set; } = string.Empty;
    public int requestedHeartbeat { get; set; } = 0;
}

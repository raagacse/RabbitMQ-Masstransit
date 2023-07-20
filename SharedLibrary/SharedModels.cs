namespace SharedLibrary;

public class Order
{
    public int productId { get; set; }
    public string productName { get; set; } = string.Empty;
    public int count { get; set; }
}

public class OrderType
{
    public int productType { get; set; }
    public string? productTypeName { get; set; }
}

public class OrderStatus
{
    public int productId { get; set; }
    public string status { get; set; } = string.Empty;
}
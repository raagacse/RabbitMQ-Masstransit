namespace SharedLibrary;

public class Order
{
    public int productId { get; set; }
    public string? productName { get; set; }
    public int count { get; set; }
}

public class OrderType
{
    public int productType { get; set; }
    public string? productTypeName { get; set; }
}
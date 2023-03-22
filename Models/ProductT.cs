

namespace TDStore.Models;
public class ProductT
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<InventoryT> Inventories { get; set; }
}

public class InventoryT
{
    public string Name { get; set; }
    public string SKU { get; set; }
    public int Quantity { get; set; }
}
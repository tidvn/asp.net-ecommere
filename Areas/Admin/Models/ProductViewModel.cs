
using TDStore.Models;

namespace TDStore.Areas.Admin.Models;
public class ProductViewModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<InventoryT> Inventories { get; set; }
}
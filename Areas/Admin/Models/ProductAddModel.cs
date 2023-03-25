
using TDStore.Models;

namespace TDStore.Areas.Admin.Models;
public class ProductAddModel
{
        public string Name { get; set; } = null!;
        public string? Details { get; set; }
        public Dictionary<string,string>? List_Specifications { get; set; }
        public string? Features { get; set; }
        public List<string>? Category { get; set; }
        public List<Product_Inventory>? List_Inventory { get; set; }

}
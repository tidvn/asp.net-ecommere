
using TDStore.Models;

namespace TDStore.Areas.Store.Models;
public class ProductView
{
        public string? Id { get; set; }    
        public string Name { get; set; } = null!;
        public string? Details { get; set; }
        public List<ImageData>? Images { get; set; }
        public Dictionary<string,string>? List_Specifications { get; set; }
        public string? Features { get; set; }
        public List<Product_Category>? Category { get; set; }
        public List<Product_Inventory>? List_Inventory { get; set; }

}
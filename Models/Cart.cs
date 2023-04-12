using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TDStore.Models
{
    public class CartItem
    {  
         public string ID {get;set;}  
         public string Name {get;set;}        
        public ImageData ImageData { get; set; }
        public Product_Inventory Inventory { get; set; }

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

    }

}







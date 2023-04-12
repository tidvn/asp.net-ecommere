using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TDStore.Models
{
    // public class Cart
    // {
    //      public string ItemId { get; set; }

    //     public string CartId { get; set; }

    //     public int Quantity { get; set; }

    //     public System.DateTime DateCreated { get; set; }
    //     public int ProductId { get; set; }

    //     public virtual Product_Inventory Product { get; set; }


    // }
    public class CartItem
    {
        public ImageData ImageData { get; set; }

        public string Name { get; set; }
        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public Product_Inventory Inventory { get; set; }
    }

}







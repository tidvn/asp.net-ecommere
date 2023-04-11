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
    {   public string Name {get;set;}
        public Product_Inventory Inventory { get; set; }
        public ImageData ImageData { get; set; }

        public decimal Quantity { get; set; }

        public decimal Discount { get; set; }

    }

}







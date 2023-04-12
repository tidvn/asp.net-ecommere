using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TDStore.Models
{
    public class CartAdd
    {
        public string idProduct {get;set;}
        public string idInventory {get;set;}
        public decimal quantily {get;set;}
    }
 

}







using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TDStore.Models;

public class Product
{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; } = null!;
        public string? Details { get; set; }
        public Dictionary<string,string>? Specifications { get; set; }
        public List<string>? Features { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string>? Category { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string>? Inventory { get; set; }
        

        
}
public class Product_Inventory
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? SKU { get; set; }
    public string? Color { get; set; }
    public string? Name { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? Images { get; set; }
    public int Quantily { get; set; }
    public decimal Price { get; set; }
    public decimal Discount_Percent { get; set; }
    

}

public class Product_Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    

}

// public class Product_Discount
// {
//     [BsonId]
//     [BsonRepresentation(BsonType.ObjectId)]
//     public string? Id { get; set; }

//     [BsonElement("Name")]
//     public string Name { get; set; } = null!;

//     public string? Description { get; set; }
    
//     public decimal Discount_Percent { get; set; }

//     public bool Active { get; set; }

// }


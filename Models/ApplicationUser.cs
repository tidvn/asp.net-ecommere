using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace TDStore.Models;

    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public User_Address[]? address { get; set;}
        public byte[]? avatar { get; set;}

    }
    public class User_Address
    {
        public string? receiver {get;set;}
        public string? phone {get;set;}
        public string? addressLine1 { get; set; }
        public string? addressLine2 { get; set; }
        public string? city { get; set; }
        public string? postalCode { get; set; }
        public string? country { get; set; }
                
}




    [CollectionName("Role")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }

    }


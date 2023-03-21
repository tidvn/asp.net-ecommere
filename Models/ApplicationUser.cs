using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace TDStore.Models;

    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {

        public string? FullName { get; set; }
        public object? Address { get; set; }
        public object? ShipingAddress { get; set; }
        public byte[]? Image { get; set;}

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


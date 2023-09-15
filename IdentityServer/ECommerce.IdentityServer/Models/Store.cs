

namespace ECommerce.IdentityServer.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ApplicationUser Owner = null!;
    }
}
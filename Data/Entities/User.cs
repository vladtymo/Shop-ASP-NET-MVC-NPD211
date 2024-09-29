using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class User : IdentityUser
    {
        // add custom properties...
        public DateTime? Birthdate { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}

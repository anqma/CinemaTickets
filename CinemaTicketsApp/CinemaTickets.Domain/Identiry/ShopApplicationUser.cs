using CinemaTickets.Domain.Domain_models;
using Microsoft.AspNetCore.Identity;

namespace CinemaTickets.Domain.Identity
{
    public class ShopApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }

        public virtual ShoppingCart UserShoppingCart { get; set; }
    }
}

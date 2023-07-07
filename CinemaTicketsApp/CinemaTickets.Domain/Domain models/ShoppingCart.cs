using System.ComponentModel.DataAnnotations;


namespace CinemaTickets.Domain.Domain_models
{

    public class ShoppingCart : BaseEntity
    {
        public string ApplicationUserId { get; set; }
        public ICollection<CinemaTicketsInShoppingCart> CinemaTicketsInShoppingCart { get; set; }

    }
}
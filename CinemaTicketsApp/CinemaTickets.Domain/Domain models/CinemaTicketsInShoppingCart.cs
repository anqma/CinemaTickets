using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTickets.Domain.Domain_models
{
    public class CinemaTicketsInShoppingCart : BaseEntity
    {
        public int CinemaTicketId { get; set; }
        public int ShoppingCartId { get; set; }
        
        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }

        [ForeignKey("CinemaTicketId")]
        public CinemaTicket CinemaTicket { get; set; }

        public int Quantity { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CinemaTickets.Domain.Domain_models
{
    public class CinemaTicket : BaseEntity
    {
       
        [Required]
        public string MovieTitle { get; set; }

        [Required]
        public string MovieImage { get; set; }

        [Required]
        public DateTime TicketTime { get; set; }

        [Required]
        public int TicketPrice { get; set; }

        public ICollection<CinemaTicketsInShoppingCart>? CinemaTicketsInShoppingCart { get; set; }
    }
}

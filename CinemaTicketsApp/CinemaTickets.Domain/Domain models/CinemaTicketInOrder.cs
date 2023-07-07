using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTickets.Domain.Domain_models
{
    public class CinemaTicketInOrder : BaseEntity
    {
        [ForeignKey("CinemaTicketId")]
        public int CinemaTicketId { get; set; }

        public CinemaTicket CinemaTicket { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}

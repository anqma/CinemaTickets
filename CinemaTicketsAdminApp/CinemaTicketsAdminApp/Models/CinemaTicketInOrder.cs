using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicketsAdminApp.Models
{
    public class CinemaTicketInOrder
    {
        public int CinemaTicketId { get; set; }
        public CinemaTicket CinemaTicket { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}

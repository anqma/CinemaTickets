namespace CinemaTicketsAdminApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ShopApplicationUser OrderedBy { get; set; }

        public List<CinemaTicketInOrder> CinemaTickets { get; set; }

    }
}

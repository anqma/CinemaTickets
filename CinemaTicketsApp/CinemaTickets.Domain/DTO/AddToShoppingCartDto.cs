namespace CinemaTickets.Domain.Domain_models.DTO
{
    public class AddToShoppingCartDto
    {
        public CinemaTicket SelectedTicket { get; set; }
        public int CinemaTicketId { get; set; }
        public int Quantity { get; set; }
    }
}

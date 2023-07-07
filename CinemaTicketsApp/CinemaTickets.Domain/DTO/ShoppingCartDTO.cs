namespace CinemaTickets.Domain.Domain_models.DTO
{
    public class ShoppingCartDTO
    {
        public List<CinemaTicketsInShoppingCart> CinemaTicketsInShoppingCart { get; set; }
        public int TotalPrice { get; set; }
    }
}

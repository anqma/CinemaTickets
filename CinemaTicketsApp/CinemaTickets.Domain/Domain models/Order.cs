
using CinemaTickets.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace CinemaTickets.Domain.Domain_models
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public ShopApplicationUser OrderedBy { get; set; }

        public List<CinemaTicketInOrder> CinemaTickets { get; set; }
    }
}

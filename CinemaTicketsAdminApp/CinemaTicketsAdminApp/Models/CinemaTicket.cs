using System.ComponentModel.DataAnnotations;

namespace CinemaTicketsAdminApp.Models
{
    public class CinemaTicket
    {
        public int Id { get; set; }
        [Required]
        public string MovieTitle { get; set; }

        [Required]
        public string MovieImage { get; set; }

        [Required]
        public DateTime TicketTime { get; set; }

        [Required]
        public int TicketPrice { get; set; }

    }
}

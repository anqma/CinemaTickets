using CinemaTickets.Domain.Domain_models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDTO GetShoppingInfo(string userId);
        bool deleteCinemaTicketFromShoppingSart(string userId,int cinemaTicketId);
        bool orderNow(string userId);

    }
}

using CinemaTickets.Domain.Domain_models;
using CinemaTickets.Domain.Domain_models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Service.Interface
{
    public interface ICinemaTicketService
    {
        List<CinemaTicket> GetCinemaTickets();
        CinemaTicket GetDetailsForCinemaTicket(int id);

        void CreateNewCinemaTicket(CinemaTicket ticket);
        void UpdateExistingCinemaTicket(CinemaTicket ticket);
        ShoppingCartDTO GetShoppingCartInfo(int id);
        void DeleteCinemaTicket(int id);
        bool AddToShoppingCart(AddToShoppingCartDto item, string userID);
    }
}

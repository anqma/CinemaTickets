using CinemaTickets.Domain.Domain_models;
using CinemaTickets.Repository.Interface;
using CinemaTickets.Domain.Domain_models.DTO;
using CinemaTickets.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Service.Implementation
{
    public class CinemaTicketService : ICinemaTicketService
    {
        public readonly IRepository<CinemaTicket> _cinemaTicketRepository;
        public readonly IUserRepository _userRepository;
        public readonly IRepository<CinemaTicketsInShoppingCart> _cinemaTicketsInShoppingCartRepository;

        public CinemaTicketService(IUserRepository userRepository, IRepository<CinemaTicketsInShoppingCart> cinemaTicketsInShoppingCartRepository, IRepository<CinemaTicket> cinemaTicketRepository)
        {
            _cinemaTicketRepository = cinemaTicketRepository;
            _userRepository = userRepository;
            _cinemaTicketsInShoppingCartRepository = cinemaTicketsInShoppingCartRepository;
        }
        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserShoppingCart;

            if (userShoppingCard != null)
            {
                var cinemaTicket = this.GetDetailsForCinemaTicket(item.CinemaTicketId);

                if (cinemaTicket != null)
                {
                    CinemaTicketsInShoppingCart itemToAdd = new CinemaTicketsInShoppingCart
                    {

                        CinemaTicket = cinemaTicket,
                        CinemaTicketId = cinemaTicket.Id,
                        ShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    /*
                    var existing = userShoppingCard.CinemaTicketsInShoppingCarts.Where(z => z.ShoppingCartId == userShoppingCard.Id && z.CinemaTicketId == itemToAdd.CinemaTicketId).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.Quantity += itemToAdd.Quantity;
                        this._cinemaTicketInShoppingCartRepository.Update(existing);

                    }
                    else
                    {
                        this._productInShoppingCartRepository.Insert(itemToAdd);
                    }*/
                    _cinemaTicketsInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewCinemaTicket(CinemaTicket ticket)
        {
            this._cinemaTicketRepository.Insert(ticket);
        }

        public void DeleteCinemaTicket(int id)
        {
            var cinemaTicket = _cinemaTicketRepository.Get(id);
            this._cinemaTicketRepository.Delete(cinemaTicket);

        }

        public List<CinemaTicket> GetCinemaTickets()
        {
            return _cinemaTicketRepository.GetAll().ToList();
        }

        public CinemaTicket GetDetailsForCinemaTicket(int id)
        {
            return _cinemaTicketRepository.Get(id);
        }

        public ShoppingCartDTO GetShoppingCartInfo(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateExistingCinemaTicket(CinemaTicket ticket)
        {
            this._cinemaTicketRepository.Update(ticket);
        }
    }
}

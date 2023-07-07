using CinemaTickets.Domain.Domain_models;
using CinemaTickets.Domain.Domain_models.DTO;
using CinemaTickets.Repository.Interface;
using CinemaTickets.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        public readonly IUserRepository _userRepository;
        public readonly IRepository<ShoppingCart> _shoppingCartRepository;
        public readonly IRepository<Order> _orderRepository;
        public readonly IRepository<CinemaTicketInOrder> _cinemaTicketInOrderRepository;


        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository, IRepository<CinemaTicketInOrder> cinemaTicketInOrderRepository)
        {
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _cinemaTicketInOrderRepository = cinemaTicketInOrderRepository;
        }
        public bool deleteCinemaTicketFromShoppingSart(string userId, int cinemaTicketId)
        {
            if(!string.IsNullOrEmpty(userId) && cinemaTicketId != null) 
            {
                var loggInUser = _userRepository.Get(userId);
                var userShoppingCart = loggInUser.UserShoppingCart;
                var itemToDelete = userShoppingCart.CinemaTicketsInShoppingCart.Where(z => z.CinemaTicketId == cinemaTicketId).FirstOrDefault();
                userShoppingCart.CinemaTicketsInShoppingCart.Remove(itemToDelete);
                _shoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            else
            {
                return false;
            }
        }

        public ShoppingCartDTO GetShoppingInfo(string userId)
        {
            var user = _userRepository.Get(userId);

            var userShoppingCart = user.UserShoppingCart;

            var cinemaTicketList = userShoppingCart.CinemaTicketsInShoppingCart.Select(z => new
            {
                Quantity = z.Quantity,
                cinemaTickerPrice = z.CinemaTicket.TicketPrice
            });

            int totalPrice = 0;

            foreach (var item in cinemaTicketList)
            {
                totalPrice += item.cinemaTickerPrice * item.Quantity;
            }

            ShoppingCartDTO model = new ShoppingCartDTO
            {
                TotalPrice = totalPrice,
                CinemaTicketsInShoppingCart = userShoppingCart.CinemaTicketsInShoppingCart.ToList()
            };
            return model; 
        }

        public bool orderNow(string userId)
        {
            var user = _userRepository.Get(userId);

            var userShoppingCart = user.UserShoppingCart;

            Order newOrder = new Order
            {
                UserId = user.Id,
                OrderedBy = user
            };
          
            _orderRepository.Insert(newOrder);

            List<CinemaTicketInOrder> cinemaTicketInOrder = userShoppingCart.CinemaTicketsInShoppingCart.Select(z => new CinemaTicketInOrder
            {
                CinemaTicket = z.CinemaTicket,
                CinemaTicketId = z.CinemaTicketId,
                Order = newOrder,
                OrderId = newOrder.Id,
                Quantity = z.Quantity
            }).ToList();
            foreach (var item in cinemaTicketInOrder)
            {
                _cinemaTicketInOrderRepository.Insert(item);
            }
            user.UserShoppingCart.CinemaTicketsInShoppingCart.Clear();
            _userRepository.Update(user);
            return true;
        }
    }
}

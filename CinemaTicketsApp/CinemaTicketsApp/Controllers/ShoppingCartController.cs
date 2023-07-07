using CinemaTickets.Domain.Domain_models;
using CinemaTickets.Domain.Domain_models.DTO;
using CinemaTickets.Repository;
using CinemaTickets.Service.Implementation;
using CinemaTickets.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CinemaTicketsApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(ApplicationDbContext context, IShoppingCartService shoppingCartService)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
        }
        public IActionResult Index()
        {
            var model = _shoppingCartService.GetShoppingInfo(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(model);

        }

        public IActionResult DeleteFromShoppingCart(int id)
        {
            _shoppingCartService.deleteCinemaTicketFromShoppingSart(User.FindFirstValue(ClaimTypes.NameIdentifier), id);
            return RedirectToAction("Index");
        }

        public IActionResult OrderNow()
        {
            _shoppingCartService.orderNow(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction("Index");
        }
    }
}

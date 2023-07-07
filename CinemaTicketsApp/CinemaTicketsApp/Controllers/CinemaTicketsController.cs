using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CinemaTickets.Service.Interface;
using CinemaTickets.Service.Implementation;
using CinemaTickets.Domain.Domain_models.DTO;
using CinemaTickets.Domain.Domain_models;

namespace CinemaTicketsApp.Controllers
{
    public class CinemaTicketsController : Controller
    {
        private readonly ICinemaTicketService _cinemaTicketsService;

        public CinemaTicketsController(ICinemaTicketService cinemaTicketsService)
        {
            _cinemaTicketsService = cinemaTicketsService;
        }

        // GET: CinemaTickets
        public async Task<IActionResult> Index()
        {
            return View(_cinemaTicketsService.GetCinemaTickets());
        }

        public async Task<IActionResult> AddToCart(int CinemaTicketId)
        {
            var cinemaTicket = _cinemaTicketsService.GetDetailsForCinemaTicket(CinemaTicketId);
            var model = new AddToShoppingCartDto();
            model.SelectedTicket = cinemaTicket;
            model.CinemaTicketId = cinemaTicket.Id;
            model.Quantity = 0;

            return View(model);
        }

        public async Task<IActionResult> AddToShoppingCart(AddToShoppingCartDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._cinemaTicketsService.AddToShoppingCart(model, userId);

            if (result)
            {
                return RedirectToAction("Index","CinemaTickets");
            }

            return RedirectToAction("Index");
        }

        // GET: CinemaTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaTicket = _cinemaTicketsService.GetDetailsForCinemaTicket(id??0);
            if (cinemaTicket == null)
            {
                return NotFound();
            }

            return View(cinemaTicket);
        }

        // GET: CinemaTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CinemaTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CinemaTicket cinemaTicket)
        {
            if (ModelState.IsValid)
            {
                _cinemaTicketsService.CreateNewCinemaTicket(cinemaTicket);
                return RedirectToAction(nameof(Index));
            }
            return View(cinemaTicket);
        }

        // GET: CinemaTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaTicket = _cinemaTicketsService.GetDetailsForCinemaTicket(id ?? 0);
            if (cinemaTicket == null)
            {
                return NotFound();
            }
            return View(cinemaTicket);
        }

        // POST: CinemaTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CinemaTicketId,MovieTitle,MovieImage,TicketTime,TicketPrice")] CinemaTicket cinemaTicket)
        {
            if (id != cinemaTicket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _cinemaTicketsService.UpdateExistingCinemaTicket(cinemaTicket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemaTicketExists(cinemaTicket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cinemaTicket);
        }

        // GET: CinemaTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaTicket = _cinemaTicketsService.GetDetailsForCinemaTicket(id ?? 0);
            if (cinemaTicket == null)
            {
                return NotFound();
            }

            return View(cinemaTicket);
        }

        // POST: CinemaTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _cinemaTicketsService.DeleteCinemaTicket(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CinemaTicketExists(int id)
        {
          return _cinemaTicketsService.GetDetailsForCinemaTicket(id) != null;
        }
    }
}

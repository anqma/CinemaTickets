using CinemaTickets.Domain.Domain_models;
using CinemaTickets.Domain.Identiry;
using CinemaTickets.Domain.Identity;
using CinemaTickets.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTickets.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ShopApplicationUser> userManager;

        public AdminController(IOrderService orderService, UserManager<ShopApplicationUser> userManager)
        {
            this.userManager = userManager;
            _orderService = orderService;
        }

        [HttpGet("[action]")]
        public List<Order> GetAllActiveOrders()
        {
            return this._orderService.getAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            return this._orderService.getOrderDetails(model);
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDTO> model)
        {
            bool status = true;

            foreach(var user in model)
            {
                var userCheck = userManager.FindByEmailAsync(user.Email).Result;
                if (userCheck != null)
                {
                    var newUser = new ShopApplicationUser
                    {
                        UserName = userCheck.Email,
                        NormalizedEmail = userCheck.Email,
                        Email = userCheck.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed= true,
                        UserShoppingCart = new ShoppingCart()
                    };

                    var result = userManager.CreateAsync(newUser, user.Password).Result;
                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }
            return status;
        }
    }
}

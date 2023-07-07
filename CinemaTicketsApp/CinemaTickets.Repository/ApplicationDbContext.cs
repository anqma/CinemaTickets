using CinemaTickets.Domain.Domain_models;
using CinemaTickets.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaTickets.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ShopApplicationUser>
    {
        public virtual DbSet<CinemaTicket> CinemaTickets { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<CinemaTicketsInShoppingCart> CinemaTicketsInShoppingCart { get; set; }
        public virtual DbSet<ShopApplicationUser> ShopApplicationUsers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<CinemaTicketInOrder> CinemaTicketInOrders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CinemaTicketsInShoppingCart>().HasKey(c => new { c.ShoppingCartId, c.CinemaTicketId });
            modelBuilder.Entity<CinemaTicketInOrder>().HasKey(c => new { c.OrderId, c.CinemaTicketId });
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
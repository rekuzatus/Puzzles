using Microsoft.EntityFrameworkCore;
using Puzzles.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Cocktail).Include(x=> x.User).ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(x => x.USerId == userId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                USerId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    CocktailId = item.Cocktail.Id,
                    OrderId = order.Id,
                    Price = item.Cocktail.Price
                };
                await _context.OrderItem.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}

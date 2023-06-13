using Puzzles.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Puzzles.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem>items, string userId, string userEmailAdress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}

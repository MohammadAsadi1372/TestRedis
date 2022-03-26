using System.Threading.Tasks;
using TestRedis.Entities;

namespace TestRedis.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task<bool> DeleteBasket(string userName);
    }
}

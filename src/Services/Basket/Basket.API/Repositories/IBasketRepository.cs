using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetbasketAsync(string userName);
        Task<ShoppingCart> UpdateBasketAsyc(ShoppingCart basket);
        Task DeleteBasketAsync(string userName);
    }
}

using Microsoft.AspNetCore.Mvc;
using TaskAuthenticationAuthorization.Models;

namespace InternetShop.Data.Repository
{
    public class ShoppingRepository : IShoppingRepository
    {   
        private readonly ShoppingContext _shoppingContext;

        public ShoppingRepository(ShoppingContext shoppingContext)
        {
            _shoppingContext = shoppingContext;
        }

        public Task<IActionResult> Create(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}

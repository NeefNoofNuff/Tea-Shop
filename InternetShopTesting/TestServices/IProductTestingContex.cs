using Microsoft.AspNetCore.Mvc;
using TaskAuthenticationAuthorization.Models;

namespace InternetShop.Data
{
    public interface ITestingContex
    {
        public Task<IActionResult> Create(Product product);
        public Task<IActionResult> Get(int id);
        public Task<IActionResult> Update(Product product);
        public Task<IActionResult> Delete(Product product);
        
    }
}

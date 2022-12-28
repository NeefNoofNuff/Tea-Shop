using InternetShop.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAuthenticationAuthorization.Models;

namespace InternetShopTesting.TestServices
{
    class TestDbProducts : IProductTestingContex
    {
        public List<Product> _storage = new List<Product>();

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

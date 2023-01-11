using InternetShop.Data.Context;
using InternetShop.Data.Models;
using InternetShop.Logic.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Logic.Repository
{
    public class OrderRepository : IOrderRepository
    {   
        private readonly ShoppingContext _context;

        public OrderRepository(ShoppingContext context)
        {
            _context = context;
        }

        public async Task Create(Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Order order)
        {
            _context.Remove(order);
            foreach (var detail in order.Details)
            {
                _context.Remove(detail);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Order> Get(int? id)
        {
            var order = _context.Orders
                .Include(x => x.Details)
                .Where(order => order.Id == id)
                .Single();

            if (order == null)
            {
                throw new NullReferenceException();
            }

            foreach (var detail in order.Details)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == detail.ProductId);
                if (product == null)
                    throw new NullReferenceException();
                detail.Product = product;
            }

            return order;
        }

        public async Task<Order> Get(string firstName, string lastName, string phoneNumber)
        {
            var orderToPass = await _context.Orders
            .Include(o => o.Details)
            .OrderBy(o => o.OrderDate)
            .LastOrDefaultAsync(o => o.FirstName == firstName
                    && o.LastName == lastName
                    && o.PhoneNumber == phoneNumber);

            if (orderToPass == null)
                throw new NullReferenceException();

            return orderToPass;
        }

        public async Task Update(Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}

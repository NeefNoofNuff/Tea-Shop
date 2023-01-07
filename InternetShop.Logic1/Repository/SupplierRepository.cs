﻿using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Data.Context;
using InternetShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Logic.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ShoppingContext _shoppingContext;
        public SupplierRepository(ShoppingContext shoppingContext)
        {
            _shoppingContext = shoppingContext;
        }

        public async Task Create(Supplier supplier)
        {
            _shoppingContext.Suppliers.Add(supplier);
            await _shoppingContext.SaveChangesAsync();
        }

        public async Task Delete(Supplier supplier)
        {
            var productsWithSupplier = _shoppingContext.Products
                .Where(product => product.SupplierId == supplier.Id)
                .ToList();

            foreach (var product in productsWithSupplier)
            {
                _shoppingContext.Products.Remove(product);
            }

            _shoppingContext.Suppliers.Remove(supplier);
            await _shoppingContext.SaveChangesAsync();
        }

        public async Task<Supplier> Get(int? id)
        {
            var supplier = await _shoppingContext.Suppliers.FirstOrDefaultAsync(supp => supp.Id == id);
            if (supplier == null)
            {
                throw new NullReferenceException("ID in supplier is null");
            }
            return supplier;
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            return await _shoppingContext.Suppliers.ToListAsync();
        }

        public async Task Update(Supplier supplier)
        {
            _shoppingContext.Update(supplier);
            await _shoppingContext.SaveChangesAsync();
        }
    }
}
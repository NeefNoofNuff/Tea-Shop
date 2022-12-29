using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.Data;
using InternetShop.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetShop.Models;

namespace InternetShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IShoppingRepository _productsRepository;
        private readonly ISupplierRepository _supplierRepository;

        public ProductsController
            (IShoppingRepository productsRepository, ISupplierRepository supplierRepository)
        {
            _productsRepository = productsRepository;
            _supplierRepository = supplierRepository;
        }
        //[Authorize(Policy = "ProductsBaseAccess")]
        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _productsRepository.GetAll());
        }
        //[Authorize(Policy = "ProductsBaseAccess")]
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsRepository.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        //[Authorize(Policy = "ProductsBaseAccess")]
        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.Suppliers = _productsRepository.GetAllSuppliers();
            return View();
        }
        //[Authorize(Policy = "ProductsBaseAccess")]
        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,UnitInStock,SupplierId")] Product product)
        {
            if (!ModelState.IsValid)
            {
                product.Supplier = await _supplierRepository.Get(product.SupplierId);               
            }
            await _productsRepository.Create(product);
            return RedirectToAction(nameof(Index));
        }
        //[Authorize(Policy = "ProductsBaseAccess")]
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Suppliers = _productsRepository.GetAllSuppliers();

            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsRepository.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        [HttpPost]
        //[Authorize(Policy = "ProductsBaseAccess")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,UnitInStock,SupplierId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                var supplier =
                    await _supplierRepository.Get(product.SupplierId);
                if(supplier == null)
                {
                    return NotFound();
                }
                product.Supplier = supplier;
            }

            try
            {
                await _productsRepository.Update(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }
        //[Authorize(Policy = "ProductsBaseAccess")]
        // GET: Products/Delete/5
        //[Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsRepository.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        //[Authorize(Policy = "ProductsBaseAccess")]
        // POST: Products/Delete/5
        // [Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productsRepository.Get(id);
            await _productsRepository.Delete(product);
            return RedirectToAction(nameof(Index));
        }

        public bool ProductExists(int id)
        {
            return _productsRepository.Exist(id);
        }
    }
}

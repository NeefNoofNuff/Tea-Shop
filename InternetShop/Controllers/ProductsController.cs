using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskAuthenticationAuthorization.Models;

namespace TaskAuthenticationAuthorization.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShoppingContext _context;

        public ProductsController(ShoppingContext context)
        {
            _context = context;
        }
        //[Authorize(Policy = "ProductsBaseAccess")]
        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }
        //[Authorize(Policy = "ProductsBaseAccess")]
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
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
            ViewBag.Suppliers = _context.Suppliers.ToList();
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
                product.Supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == product.SupplierId);               
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[Authorize(Policy = "ProductsBaseAccess")]
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Suppliers = _context.Suppliers.ToList();

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
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
                    await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == product.SupplierId);
                if(supplier == null)
                {
                    return NotFound();
                }
                product.Supplier = supplier;
            }

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
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

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

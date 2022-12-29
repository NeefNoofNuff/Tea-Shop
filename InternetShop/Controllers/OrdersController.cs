//using System;
//using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetShop.Models;

namespace InternetShop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ShoppingContext _context;

        public OrdersController(ShoppingContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            //var orders = _context.Orders.Include(o => o.Customer).Include(o => o.SuperMarket)
            //   .Where(o => User.IsInRole(ShoppingContext.ADMIN_ROLE_NAME) || 
            //               User.IsInRole(ShoppingContext.BUYER_ROLE_NAME) && o.Customer.User.Login == User.Identity.Name);


            return View();
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var order = await _context.Orders
            //    .Include(o => o.SuperMarket)
            //    .Include(o => o.Customer)
            //    .ThenInclude(cust => cust.User)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (order == null)
            //{
            //    return NotFound();
            //}
            //if (User.IsInRole(ShoppingContext.BUYER_ROLE_NAME) && order.Customer.User.Login != User.Identity.Name)
            //{
            //    return NotFound();
            //}
            return View(/*order*/);
        }

        //        //[Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        //        // GET: Orders/Create
        //        public IActionResult Create()
        //        {
        //            //ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
        //            //ViewData["SuperMarketId"] = new SelectList(_context.SuperMarkets, "Id", "Id");
        //            return View();
        //        }

        //        // POST: Orders/Create
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Create([Bind("Id,OrderDate,CustomerId,SuperMarketId")] Order order)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                _context.Add(order);
        //                await _context.SaveChangesAsync();
        //                return RedirectToAction(nameof(Index));
        //            }
        //            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
        //            //ViewData["SuperMarketId"] = new SelectList(_context.SuperMarkets, "Id", "Id", order.SuperMarketId);
        //            return View(order);
        //        }

        //        //[Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        //        // GET: Orders/Edit/5
        //        public async Task<IActionResult> Edit(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return NotFound();
        //            }

        //            var order = await _context.Orders.FindAsync(id);
        //            if (order == null)
        //            {
        //                return NotFound();
        //            }
        //            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
        //            ViewData["SuperMarketId"] = new SelectList(_context.SuperMarkets, "Id", "Id", order.SuperMarketId);
        //            return View(order);
        //        }

        //        // POST: Orders/Edit/5
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderDate,CustomerId,SuperMarketId")] Order order)
        //        {
        //            if (id != order.Id)
        //            {
        //                return NotFound();
        //            }

        //            if (ModelState.IsValid)
        //            {
        //                try
        //                {
        //                    _context.Update(order);
        //                    await _context.SaveChangesAsync();
        //                }
        //                catch (DbUpdateConcurrencyException)
        //                {
        //                    if (!OrderExists(order.Id))
        //                    {
        //                        return NotFound();
        //                    }
        //                    else
        //                    {
        //                        throw;
        //                    }
        //                }
        //                return RedirectToAction(nameof(Index));
        //            }
        //            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
        //            //ViewData["SuperMarketId"] = new SelectList(_context.SuperMarkets, "Id", "Id", order.SuperMarketId);
        //            return View(order);
        //        }
        //        //[Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        //        // GET: Orders/Delete/5
        //        public async Task<IActionResult> Delete(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return NotFound();
        //            }

        //            //var order = await _context.Orders
        //            //    .Include(o => o.Customer)
        //            //    .Include(o => o.SuperMarket)
        //            //    .FirstOrDefaultAsync(m => m.Id == id);
        //            if (order == null)
        //            {
        //                return NotFound();
        //            }

        //            return View(order);
        //        }

        //        // POST: Orders/Delete/5
        //        [Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> DeleteConfirmed(int id)
        //        {
        //            var order = await _context.Orders.FindAsync(id);
        //            _context.Orders.Remove(order);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }

        //        private bool OrderExists(int id)
        //        {
        //            return _context.Orders.Any(e => e.Id == id);
        //        }
    }
}

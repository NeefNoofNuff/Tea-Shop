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
using InternetShop.Data.Repository;
using InternetShop.Services;

namespace InternetShop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ShoppingContext _context;
        private readonly IShoppingRepository _shoppingRepository;

        public OrdersController
            (ShoppingContext context, IShoppingRepository shoppingRepository)
        {
            _context = context;
            _shoppingRepository = shoppingRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View("create");
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(/*order*/);
        }

        [HttpGet("Orders/Create")]
        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        public async Task<IActionResult> Create(int id, 
            [Bind("Id,OrderDate,FirstName,LastName,PhoneNumber,ProductId,UnitsCount")] Order order)
        {
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            if (!ModelState.IsValid)
            {
                order.Product = _context.Products.Find(order.ProductId);
                var priceCalc = order.Product.Price * order.UnitsCount;
                order.Price = priceCalc.ToString();
            }
            var reductionResult = _shoppingRepository.ReduceUnitStockAsync(order.Product, order.UnitsCount);
            if (!Task.FromResult(reductionResult).Result.Result)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Add(order);
            await InvoiceFactory.Create(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

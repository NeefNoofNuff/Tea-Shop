using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InternetShop.Data.Context;
using InternetShop.Data.Models;
using InternetShop.Logic.Services;
using InternetShop.Logic.Repository.Interfaces;

namespace InternetShop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ShoppingContext _context;
        private readonly IShoppingRepository _shoppingRepository;
        private readonly InvoiceFactory _invoiceFactory;

        public OrdersController
            (ShoppingContext context, IShoppingRepository shoppingRepository, InvoiceFactory invoiceFactory)
        {
            _context = context;
            _shoppingRepository = shoppingRepository;
            _invoiceFactory = invoiceFactory;
        }

        public IActionResult Index()
        {
            return View("create");
        }

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
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
                var product = _context.Products.Find(order.ProductId);
                if(product == null)
                {
                    return RedirectToAction(nameof(NotPlaced));
                }
                order.Product = product;
                var priceCalc = order.Product.Price * order.UnitsCount;
                order.Price = priceCalc.ToString();
            }
            var reductionResult = await _shoppingRepository.ReduceUnitStockAsync(order.Product, order.UnitsCount);
            if (!reductionResult)
            {
                return RedirectToAction(nameof(NotPlaced));
            }
            _context.Add(order);
            await _invoiceFactory.Create(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Placed));
        }

        public IActionResult Placed()
        {
            return View();
        }

        public IActionResult NotPlaced()
        {
            return View();
        }
    }
}

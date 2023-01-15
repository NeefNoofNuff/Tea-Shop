using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InternetShop.Data.Models;
using InternetShop.Logic.Services.Interfaces;
using InternetShop.Presentation.Filters.Exceptions;

namespace InternetShop.Controllers
{
    [Authorize]
    [TypeFilter(typeof(DbConnectionExceptionAttribute))]
    public class OrdersController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public OrdersController
            (IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View("create");
        }

        [HttpGet("Orders/Create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Products = new SelectList
                (await _productService.GetAll(), "Id", "Name");
                return View();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] Order order)
        {
            ViewBag.Products = new SelectList
                (await _productService.GetAll(), "Id", "Name");

            await _orderService.CreateDefaultOrder(order);
           
            return RedirectToAction("Edit", new {id = order.Id});
        }

        [HttpGet("Orders/Edit/{id}")]
        public async Task<IActionResult> EditDetails(int? id)
        {
            var order = await _orderService.Get(id);
            return View(order.Details);
        }

        [HttpPost("Orders/Edit/{id}")]
        public async Task<IActionResult> EditDetails
            (int? id, [FromForm] List<OrderDetail> details)
        {   
            if (id == null || details == null)
                return RedirectToAction(nameof(NotPlaced));

            try
            {
                var orderId = await _orderService.UpdateDetails(id, details);
                if(orderId == null)
                {
                    throw new NullReferenceException("Order was not updated!");
                }
                return RedirectToAction("Show", new { id = orderId });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet("Orders/Show/{id}")]
        public async Task<IActionResult> Show(int? id)
        {
            try
            {
                var order = await _orderService.Get(id);
                return View(order);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet("Orders/Placed/{id}")]
        public async Task<IActionResult> Placed(int? id)
        {
            try
            {
                await _orderService.CreateInvoice(id);
                return View();
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpGet("Orders/NotPlaced/{id}")]
        public async Task<IActionResult> NotPlaced(int? id)
        {
            await _orderService.Delete(id);
            return View();
        }
    }
}

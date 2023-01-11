using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InternetShop.Data.Context;
using InternetShop.Data.Models;
using InternetShop.Logic.Services;
using InternetShop.Logic.Repository.Interfaces;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using InternetShop.Logic.Repository;

namespace InternetShop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IShoppingRepository _shoppingRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly InvoiceFactory _invoiceFactory;

        public OrdersController
            (IShoppingRepository shoppingRepository, IOrderRepository orderRepository,
            InvoiceFactory invoiceFactory)
        {
            _shoppingRepository = shoppingRepository;
            _invoiceFactory = invoiceFactory;
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            return View("create");
        }

        [HttpGet("Orders/Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = new SelectList
                (await _shoppingRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] Order order)
        {
            ViewBag.Products = new SelectList
                (await _shoppingRepository.GetAll(), "Id", "Name");
            if (!ModelState.IsValid)
            {
                order.Details = new List<OrderDetail>(order.ProductsId.Count);
                order.Price = "0";
                order.OrderDate = DateTime.Now;
            }

            await _orderRepository.Create(order);

            var orderToPass = await _orderRepository.Get(order.FirstName, order.LastName, order.PhoneNumber);

            if(orderToPass == null)
            {
                return RedirectToAction(nameof(NotPlaced));
            }

            foreach (var prodId in order.ProductsId)
            {
                var product = await _shoppingRepository.Get(prodId);
                if (product == null)
                {
                    return RedirectToAction(nameof(NotPlaced));
                }
                orderToPass.Details.Add(new OrderDetail(order, order.Id, product, prodId, 0));
            }

            foreach (var detail in orderToPass.Details)
            {
                detail.OrderId = order.Id;
                detail.Order = order;
            }

            order = orderToPass;
            await _orderRepository.Update(order);
            return RedirectToAction("Edit", new {id = order.Id});
        }

        [HttpGet("Orders/Edit/{id}")]
        public async Task<IActionResult> EditDetails(int? id)
        {
            var order = await _orderRepository.Get(id);

            if (order == null 
                || order.Details == null 
                || !order.Details.Any())
            {
                return RedirectToAction(nameof(NotPlaced));
            }

            return View(order.Details);
        }

        [HttpPost("Orders/Edit/{id}")]
        public async Task<IActionResult> EditDetails
            (int? id, [FromForm] List<OrderDetail> details)
        {   
            if (id == null || details == null)
                return RedirectToAction(nameof(NotPlaced));

            var order = await _orderRepository.Get(id);
            order.Details = details;

            foreach (var detail in order.Details)
            {
                var product = await _shoppingRepository.Get(detail.ProductId);
                if (product == null)
                    return RedirectToAction(nameof(NotPlaced));
                detail.Product = product;
            }

            order.Price = order.Details
                .Select(x => x.Product.Price * x.Units)
                .Sum()
                .ToString();

            if (_shoppingRepository.ReduceUnitStockAsync(order.Details))
            {
                await _orderRepository.Update(order);
            }

            return RedirectToAction("Show", new { id = order.Id });

        }
        [HttpGet("Orders/Show/{id}")]
        public async Task<IActionResult> Show(int? id)
        {
            var order = await _orderRepository.Get(id);
            if (order == null || order.Details == null)
                return NotFound();
            return View(order);
        }
        [HttpGet("Orders/Placed/{id}")]
        public async Task<IActionResult> Placed(int? id)
        {
            var order = await _orderRepository.Get(id);
            if (order == null || order.Details == null)
                return NotFound();
            order.Confirmed = true;
            await _orderRepository.Update(order);
            await _invoiceFactory.Create(order);
            return View();
        }

        [HttpGet("Orders/NotPlaced/{id}")]
        public async Task<IActionResult> NotPlaced(int? id)
        {
            var order = await _orderRepository.Get(id);
            if (order == null || order.Details == null)
                return NotFound();
            await _orderRepository.Delete(order);
            return View();
        }
    }
}

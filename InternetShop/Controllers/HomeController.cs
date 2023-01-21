using InternetShop.Data.Context;
using InternetShop.Data.Models;
using InternetShop.Logic.Services.Interfaces;
using InternetShop.Presentation.Filters.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InternetShop.Controllers
{
    [TypeFilter(typeof(ExceptionFilterAttribute))]
    public class HomeController : Controller
    {   
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAll());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
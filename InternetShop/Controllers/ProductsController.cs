using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternetShop.Data.Models;
using InternetShop.Logic.Repository.Interfaces;
using X.PagedList;
using InternetShop.Logic.Services;
using InternetShop.Logic.Services.Interfaces;

namespace InternetShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;

        public ProductsController(IProductService productService, ISupplierService supplierService)
        {
            _productService = productService;
            _supplierService = supplierService;
        }

        public async Task<IActionResult> Index
            (string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? PagingTools.SortingByNameDesc : "";
            ViewBag.PriceSortParm = sortOrder == PagingTools.SortingByPrice ? PagingTools.SortingByPriceDesc : PagingTools.SortingByPrice;
            var products = await _productService.GetAll();
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products
                    .Where(product => product.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case PagingTools.SortingByNameDesc:
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case PagingTools.SortingByPrice:
                    products = products.OrderBy(s => s.Price);
                    break;
                case PagingTools.SortingByPriceDesc:
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, PagingTools.ElementsPerPage));
        }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            try
            {
                var product = await _productService.Get(id);
                return View(product);
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        [Authorize(Policy = "WriteAccess")]
        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Suppliers = await _supplierService.GetAll();
            return View();
        }
        // POST: Products/Create
        [HttpPost]
        [Authorize(Policy = "WriteAccess")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    product.Supplier = await _supplierService.Get(product.SupplierId);
                }
                await _productService.Create(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        [Authorize(Policy = "WriteAccess")]
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Suppliers = _supplierService.GetAll();

            if (id == null)
            {
                throw new NullReferenceException("Id for product in edit is null!");
            }
            try
            {
                var product = await _productService.Get(id);
                return View(product);
            }
            catch (Exception)
            {
                return View("Error");
            }


        }

        // POST: Products/Edit/5
        [HttpPost]
        [Authorize(Policy = "WriteAccess")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,UnitInStock,SupplierId")] Product product)
        {
            if (id != product.Id)
            {
                return View("Error");
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    var supplier =
                        await _supplierService.Get(product.SupplierId);
                    product.Supplier = supplier;
                }

                await _productService.Update(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return View("Error");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));

        }
        [Authorize(Policy = "WriteAccess")]
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            try
            {
                var product = await _productService.Get(id);
                return View(product);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        // POST: Products/Delete/5
        [Authorize(Policy = "WriteAccess")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await _productService.Get(id);
                await _productService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public bool ProductExists(int id)
        {
            try
            {
                var result = _productService.Get(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

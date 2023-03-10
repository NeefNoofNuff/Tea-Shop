using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternetShop.Data.Models;
using InternetShop.Logic.Services;
using InternetShop.Logic.Services.Interfaces;
using InternetShop.Presentation.Filters.Exceptions;

namespace InternetShop.Controllers
{
    [TypeFilter(typeof(ExceptionFilterAttribute))]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        private readonly IPaging _pagingTools;

        public ProductsController(IProductService productService, ISupplierService supplierService, IPaging pagingTools)
        {
            _productService = productService;
            _supplierService = supplierService;
            _pagingTools = pagingTools;
        }

        public async Task<IActionResult> Index
            (string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? PagingTools.SortingByNameDesc : "";
            ViewBag.PriceSortParm = sortOrder == PagingTools.SortingByPrice ? PagingTools.SortingByPriceDesc : PagingTools.SortingByPrice;
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var pagedCollection = await _pagingTools.PageViewProductAsync(sortOrder, searchString, page);

            if(pagedCollection == null || pagedCollection.Count == 0)
            {
                return NoContent();
            }
            return View(pagedCollection);
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
            ViewBag.Suppliers = await _supplierService.GetAll();

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

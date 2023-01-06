using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternetShop.Models;
using InternetShop.Logic.Repository.Interfaces;
using X.PagedList;

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
        public async Task<IActionResult> Index
            (string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Date";
            var products = await _productsRepository.GetAll();
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
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
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
        [Authorize(Policy = "WriteAccess")]
        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.Suppliers = _productsRepository.GetAllSuppliers();
            return View();
        }
        // POST: Products/Create
        [HttpPost]
        [Authorize(Policy = "WriteAccess")]
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
        [Authorize(Policy = "WriteAccess")]
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
        [HttpPost]
        [Authorize(Policy = "WriteAccess")]
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
        [Authorize(Policy = "WriteAccess")]
        // GET: Products/Delete/5
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
        // POST: Products/Delete/5
        [Authorize(Policy = "WriteAccess")]
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

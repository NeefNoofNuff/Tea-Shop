using InternetShop.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternetShop.Logic.Services.Interfaces;
using InternetShop.Logic.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace InternetShop.Controllers
{
    public class SupplierController : Controller
    {   
        private readonly ISupplierService _supplierService;
        private readonly PagingTools _pagingTools;
        public SupplierController(ISupplierService supplierService, PagingTools pagingTools)
        {
            _supplierService = supplierService;
            _pagingTools = pagingTools;
        }

        [Authorize(Policy = "SupplierAccess")]
        // GET: SupplierController
        public async Task<IActionResult> Index
            (string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? PagingTools.SortingByNameDesc : "";
            ViewBag.PriceSortParm = sortOrder == PagingTools.SortingByPrice ? PagingTools.SortingByPriceDesc : PagingTools.SortingByPrice;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var pagedCollection = await _pagingTools.PageViewSupplierAsync(sortOrder, searchString, page);

            if (pagedCollection == null || pagedCollection.Count == 0)
            {
                return NoContent();
            }
            return View(pagedCollection);
        }
        [Authorize(Policy = "SupplierAccess")]
        // GET: SupplierController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var supplier =
                await _supplierService.Get(id);

            if(supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }
        [Authorize(Policy = "SupplierAccess")]

        // GET: SupplierController/Create
        public ActionResult Create()
        {   
            return View();
        }

        // POST: SupplierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "SupplierAccess")]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,CompanyName,PhoneNumber")] Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            try
            {
                await _supplierService.Create(supplier);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View("Index");
            }
        }

        // GET: SupplierController/Edit/5
        [Authorize(Policy = "SupplierAccess")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var supp =
                await _supplierService.Get(id);
            return View(supp);
        }

        // POST: SupplierController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "SupplierAccess")]
        public async Task<IActionResult> Edit(int? id, 
            [Bind("Id,FirstName,LastName,CompanyName,PhoneNumber")] Supplier supplier)
        {   
            if (!ModelState.IsValid || id == null)
            {
                return View();
            }
            try
            {
                await _supplierService.Update(supplier);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SupplierController/Delete/5
        [Authorize(Policy = "SupplierAccess")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _supplierService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: SupplierController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "SupplierAccess")]
        public async Task<IActionResult> Delete(int? id, [FromForm] Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _supplierService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

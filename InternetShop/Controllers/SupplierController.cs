using InternetShop.Data;
using InternetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAuthenticationAuthorization.Models;

namespace InternetShop.Controllers
{
    public class SupplierController : Controller
    {   
        private readonly ShoppingContext _shoppingContext;
        //SupplierAccess
        
        public SupplierController(ShoppingContext shoppingContext)
        {
            _shoppingContext = shoppingContext;
        }
        [Authorize(Policy = "SupplierAccess")]
        // GET: SupplierController
        public async Task<IActionResult> Index()
        {
            return View(await _shoppingContext.Suppliers.ToListAsync());
        }
        [Authorize(Policy = "SupplierAccess")]
        // GET: SupplierController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var supplier = 
                await _shoppingContext.Suppliers.FirstOrDefaultAsync(supp => supp.Id == id);

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
                await _shoppingContext.AddAsync(supplier);
                await _shoppingContext.SaveChangesAsync();
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
                await _shoppingContext.Suppliers
                .FirstOrDefaultAsync(supp => supp.Id == id);
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
                _shoppingContext.Update(supplier);
                await _shoppingContext.SaveChangesAsync();
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

            var supp = await _shoppingContext.Suppliers
                .FirstOrDefaultAsync(supp => supp.Id == id);
            if (supp == null)
            {
                return NotFound();
            }

            return View(supp);
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
                _shoppingContext.Suppliers.Remove(supplier);
                await _shoppingContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

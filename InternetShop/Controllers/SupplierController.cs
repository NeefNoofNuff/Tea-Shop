using InternetShop.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternetShop.Logic.Repository.Interfaces;

namespace InternetShop.Controllers
{
    public class SupplierController : Controller
    {   
        private readonly ISupplierRepository _supplierRepository;
        //SupplierAccess
        
        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        [Authorize(Policy = "SupplierAccess")]
        // GET: SupplierController
        public async Task<IActionResult> Index()
        {
            return View(await _supplierRepository.GetAll());
        }
        [Authorize(Policy = "SupplierAccess")]
        // GET: SupplierController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var supplier =
                await _supplierRepository.Get(id);

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
                await _supplierRepository.Create(supplier);
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
                await _supplierRepository.Get(id);
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
                await _supplierRepository.Update(supplier);
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

            var supp = await _supplierRepository.Get(id);
            if (supp == null)
            {
                return NotFound();
            }
            await _supplierRepository.Delete(supp);
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
                await _supplierRepository.Delete(supplier);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

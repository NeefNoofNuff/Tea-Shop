using InternetShop.Data;
using InternetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAuthenticationAuthorization.Models;

namespace InternetShop.Controllers
{
    public class MapController : Controller
    {
        private readonly MapContext _context;

        public MapController(MapContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Index()
        {
            return View(_context.GetAll());
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST

        //[Authorize(Policy = "ProductsBaseAccess")]
        [HttpPost]

        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Address,Hours")] Shop? shop)
        {
            if (ModelState.IsValid)
            {
                if (shop == null) return View("Index");
                _context.Add(shop);
                return RedirectToAction(nameof(Index));
            }
            return View(shop);
        }

        //[Authorize(Policy = "ProductsBaseAccess")]
        // GET: Products/Edit/5
        //[Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var shop = _context.Find(id);

            return shop == null ? NotFound() : View(shop);
        }

        //[Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        [HttpPost]
        //[Authorize(Policy = "ProductsBaseAccess")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Address,Hours")] Shop shop)
        {
            //if (_context.Exists(shop.Id)) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(shop);
                return RedirectToAction(nameof(Index));
            }
            return View(shop);
        }

        //[Authorize(Policy = "ProductsBaseAccess")]
        // GET: Products/Delete/5
        //[Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = _context.Find(id);

            return product == null ? NotFound() : View(product);
        }

        //[Authorize(Policy = "ProductsBaseAccess")]
        // POST: Products/Delete/5
        // [Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

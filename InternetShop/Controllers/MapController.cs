using InternetShop.Data.Models;
using InternetShop.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    public class MapController : Controller
    {
        private readonly MapContext _context;

        public MapController(MapContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.GetAll().ToList().OrderBy(x => x.Id));
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Policy = "WriteAccess")]
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
            if (ModelState.IsValid)
            {
                _context.Update(shop);
                return RedirectToAction(nameof(Index));
            }
            return View(shop);
        }

        //[Authorize(Policy = "ProductsBaseAccess")]
        //[Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = _context.Find(id);

            return product == null ? NotFound() : View(product);
        }

        //[Authorize(Policy = "ProductsBaseAccess")]
        // [Authorize(Roles = ShoppingContext.ADMIN_ROLE_NAME)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

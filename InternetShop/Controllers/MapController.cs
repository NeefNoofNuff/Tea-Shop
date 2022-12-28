using InternetShop.Data;
using InternetShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    public class MapController : Controller
    {
        private readonly MapContext _context;

        public List<Shop> shops { get; set; }

        public MapController(MapContext context)
        {
            _context = context;

            shops = _context.GetAll();
        }

        
        // GET: MapController
        public IActionResult Index()
        {
            return View(shops);
        }


        /*// GET: MapController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MapController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MapController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: MapController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MapController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: MapController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}

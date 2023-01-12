using InternetShop.Data.Models;
using InternetShop.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternetShop.Logic.Services.Interfaces;

namespace InternetShop.Controllers
{
    public class MapController : Controller
    {
        private readonly IMapService _mapService;

        public MapController(IMapService mapService)
        {
            _mapService = mapService;
        }

        public IActionResult Index()
        {
            try
            {
                return View(_mapService.GetAll());
            }
            catch (Exception)
            {
                return NotFound();
            }
            
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
            try
            {
                if (ModelState.IsValid)
                {
                    if (shop == null) return View("Index");
                    _mapService.Create(shop);
                    return RedirectToAction(nameof(Index));
                }
                return View(shop);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [Authorize(Policy = "WriteAccess")]
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null) return NotFound();

                var shop = _mapService.Get(id);

                return shop == null ? NotFound() : View(shop);
            }
            catch (Exception)
            {
                return NotFound();  
            }
        }
        [Authorize(Policy = "WriteAccess")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Address,Hours")] Shop shop)
        {
            if (ModelState.IsValid)
            {
                _mapService.Update(shop);
                return RedirectToAction(nameof(Index));
            }
            return View(shop);
        }
        [Authorize(Policy = "WriteAccess")]
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null) return NotFound();

                var product = _mapService.Get(id);

                return product == null ? NotFound() : View(product);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [Authorize(Policy = "WriteAccess")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            try
            {
                _mapService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
    }
}

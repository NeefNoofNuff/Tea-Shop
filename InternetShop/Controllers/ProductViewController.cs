using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Presentation.Controllers
{
    public class ProductViewController : Controller
    {   
        // product view controller

        // GET: ProductViewController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductViewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductViewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductViewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ProductViewController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductViewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ProductViewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductViewController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}

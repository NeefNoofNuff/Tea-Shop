using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    public class DiscountController : Controller
    {
        public IActionResult Details()
        {
            return Content("Your discount is 5%");
        }
    }
}

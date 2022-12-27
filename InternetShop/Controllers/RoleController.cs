using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InternetShop.Controllers
{
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [Authorize(Policy = "EditRole")]
        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        [Authorize(Policy = "EditRole")]
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }
        [Authorize(Policy = "EditRole")]
        public IActionResult Delete()
        {
            return View();
        }
        [Authorize(Policy = "EditRole")]
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
        [Authorize(Policy = "EditRole")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] string? name)
        {
            var role = await roleManager.FindByNameAsync(name);
            if(role != null)
            {
                await roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Delete");
        }
    }
}

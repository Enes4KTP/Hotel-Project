using HotelProject.EntityLayer.Concrete;
using HotelProject.WebUI.Models.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
    public class RoleAssignController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RoleAssignController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> AssingRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                ViewData["__ERROR__"] = "Not found user!";
                return View();
            }
            TempData["userid"] = user?.Id;
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            List<RoleAssingViewModel> roleAssingViewModels = new List<RoleAssingViewModel>();
            foreach(var item in roles)
            {
                RoleAssingViewModel role = new RoleAssingViewModel();
                role.RoleID = item.Id;
                role.RoleName = item.Name;
                role.RoleExist = userRoles.Contains(item.Name);
                roleAssingViewModels.Add(role);
            }
            return View(roleAssingViewModels);
        }
        [HttpPost]
        public async Task<IActionResult> AssingRole(List<RoleAssingViewModel> roleAssingViewModel)
        {
            var userid = (int)TempData["userid"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);
            foreach(var item in roleAssingViewModel)
            {
                if (item.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("Index");
        }

    }
}

using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DegitalDelight.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPictureService _pictureService;
        public AccountController(IUserService userService, IPictureService pictureService)
        {
            _userService = userService;
            _pictureService = pictureService;
        }
        public async Task<IActionResult> Account()
        {
            return View(await _userService.GetUserList());
        }

        public class Role
        {
            public Role(string name, int value)
            {
                Name = name;
                Value = value;
            }
            public string Name { get; set; }
            public int Value { get; set; }

        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            var currentUser = await _userService.GetCurrentUser();
            ViewBag.SameUser = currentUser.Id == id;
            var currentRole = await _userService.IsAdmin(id) ? 0 : 1;
            List<Role> roles = new List<Role> { new Role("Administrator", 0), new Role("Customer", 1) };
            ViewBag.SelectType = new SelectList(roles, "Value", "Name", currentRole);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser([Bind("Id,UserName,PhoneNumber,Email")] User user, int role, IFormFile fileInput)
        {
            
            var currentRole = await _userService.IsAdmin(user.Id) ? 0 : 1;
            List<Role> roles = new List<Role> { new Role("Administrator", 0), new Role("Customer", 1) };
            ViewBag.SelectType = new SelectList(roles, "Value", "Name", currentRole);
            var currentUser = await _userService.GetCurrentUser();
            ViewBag.SameUser = currentUser.Id == user.Id;
            if (ModelState.IsValid)
            {
                if (fileInput != null)
                {
                    user.ImagePath = _pictureService.CreateAccountPicture(fileInput);
                }
                if (role != currentRole)
                {
                    await _userService.EditUserRole(user, role);
                }
                var changedResult = await _userService.EditUser(user);
                if (changedResult)
                {
                    return RedirectToAction("Account");
                }
                return View(user);
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> RevertState(string id)
        {
            await _userService.RevertState(id);
            return RedirectToAction("Account");
        }
    }
}

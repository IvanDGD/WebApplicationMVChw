using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserValidation.ViewModels;
using UserValidation.Models;
using UserValidation.Data;

namespace UserValidation.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;

        public AccountController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!string.IsNullOrWhiteSpace(model.Username))
            {
                bool exists = await _db.Users.AnyAsync(u => u.Username == model.Username);
                if (exists)
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is existed");
                    return View(model);
                }
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Username = model.Username,
                Password = model.Password
            };

            try
            {
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Error saving. Maybe, Username is exited.");
                return View(model);
            }

            TempData["Success"] = "Register is success!";
            return RedirectToAction("RegisterSuccess");
        }

        [HttpGet]
        public IActionResult RegisterSuccess()
        {
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsUsernameAvailable(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Json(true);

            bool exists = await _db.Users.AnyAsync(u => u.Username == username);
            return Json(!exists);
        }
    }
}
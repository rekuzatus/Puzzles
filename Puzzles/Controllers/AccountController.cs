using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Puzzles.Data;
using Puzzles.Data.Static;
using Puzzles.Data.ViewModels;
using Puzzles.Models;
using System.Threading.Tasks;

namespace Puzzles.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager= userManager;
            _signInManager= signInManager;
            _context= context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAdress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Cocktails");
                    }
                }
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }

        public IActionResult Signin()
        {
            return View(new SigninVM());
        }

        [HttpPost]
        public async Task<IActionResult> Signin(SigninVM signinVM)
        {
            if (!ModelState.IsValid) return View(signinVM);

            var user = await _userManager.FindByEmailAsync(signinVM.EmailAdress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(signinVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = signinVM.Name,
                Email = signinVM.EmailAdress,
                UserName = signinVM.EmailAdress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, signinVM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("SignInCompleted");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Cocktails");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}

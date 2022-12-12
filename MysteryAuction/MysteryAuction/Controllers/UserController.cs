using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MysteryAuction.Core.Constants;
using MysteryAuction.Core.Models.User;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<MysteryAuctionUser> userManager;
        private readonly SignInManager<MysteryAuctionUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<MysteryAuctionUser> _userManager,
            SignInManager<MysteryAuctionUser> _signInManager,
            RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                //TODO: Add redirection
                return Ok();
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var sanitizer = new HtmlSanitizer();
            model.Email = sanitizer.Sanitize(model.Email);
            model.UserName = sanitizer.Sanitize(model.UserName);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new MysteryAuctionUser()
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //TODO: Add redirection
                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            //TODO: 
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                //TODO: Add redirection
                return Ok();
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var sanitizer = new HtmlSanitizer();
            model.UserName = sanitizer.Sanitize(model.UserName);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                
                //TODO: Add redirection
                if (result.Succeeded)
                {
                    return Ok();
                }
            }

            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        //Used to register admin user

        //public async Task<IActionResult> CreateRoles()
        //{

        //    await roleManager.CreateAsync(new IdentityRole(RoleConstants.Admin));

        //    return RedirectToAction("Index", "Home");
        //}
        ////Add role to the user found by the given email.
        //public async Task<IActionResult> AddUsersToRoles()
        //{
        //    const string  email = "admin@mail.com";


        //    var user = await userManager.FindByNameAsync(email);


        //    await userManager.AddToRolesAsync(user, new string[] { RoleConstants.Admin });

        //    return RedirectToAction("Index", "Home");
        //}
    }
}

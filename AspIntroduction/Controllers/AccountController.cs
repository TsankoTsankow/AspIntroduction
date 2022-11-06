using AspIntroduction.Core.Data.Models;
using AspIntroduction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspIntroduction.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
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
            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FistName,
                LastName = model.LastName,
                EmailConfirmed = true,
                UserName = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("first_name", user.FirstName ?? user.Email));


            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {

                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    if (model.ReturnUrl != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid Login");

            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRoles()
        {
            await roleManager.CreateAsync(new IdentityRole("Manager"));
            await roleManager.CreateAsync(new IdentityRole("Supervisor"));
            await roleManager.CreateAsync(new IdentityRole("Administrator"));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ListAllUsers()
        {
            //string email1 = "ts.tsankow@gmail.com";
            //string email2 = "pesho@abv.bg";

            //var user1 = await userManager.FindByEmailAsync(email1);
            //var user2 = await userManager.FindByEmailAsync(email2);

            //await userManager.AddToRoleAsync(user2, "Manager");
            //await userManager.AddToRolesAsync(user1, new string[] {"Supervisor", "Administrator"});

            var allUsers = await userManager.Users.ToListAsync();

            List<AllUsersViewModel> allUsersViewModel = new List<AllUsersViewModel>();

            foreach (var user in allUsers)
            {
                AllUsersViewModel curUser = new AllUsersViewModel()
                {
                    Name = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    Role = "None"
                };

                allUsersViewModel.Add(curUser);
            }

            return View(allUsersViewModel);
            //return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddRoleToUser(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            await userManager.AddToRoleAsync(user, "Supervisor");

            return RedirectToAction(nameof(ListAllUsers));
        }

        public async Task<IActionResult> RemoveRoleFromUser(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            var roles = await userManager.GetRolesAsync(user);


            foreach (var role in roles)
            {

                await userManager.RemoveFromRoleAsync(user, role);

            }

            return RedirectToAction(nameof(ListAllUsers));
        }
    }
}

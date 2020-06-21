using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopping.Models;
using OnlineShopping.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShopping.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<UserTypeClass> userManager;
        private readonly SignInManager<UserTypeClass> signInManager;
        private readonly IUserRepository userRepository;

        public AccountController(UserManager<UserTypeClass> userManager,
            SignInManager<UserTypeClass> signInManager, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("about", "account", new { @id = userManager.GetUserName(User) });
            }

        }

        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string usertype = model.UserType.ToString();
                if(usertype == "Admin")
                {
                    model.UserID = "ADM" + GenerateRandomNo().ToString();
                }
                else
                {
                    model.UserID = "CUST" + GenerateRandomNo().ToString();
                }

                var user = new UserTypeClass()
                {
                    UserType = usertype[0],
                    UserName = model.UserID,
                };

                UserDetails userdetail = new UserDetails()
                {
                    UserID=model.UserID,
                    Name = model.Name,
                    Address = model.Address,
                    Country = model.Country.ToString(),
                    Pin = model.Pin,
                    Email = model.Email,
                    Phone = model.Phone,
                    Gender = model.Gender.ToString(),
                    LanguagesKnown= Request.Form["Language"],
                };

                

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    userRepository.AddUser(userdetail);
                    HttpContext.Session.SetString("username", model.UserID);
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("about", "account", new { @id = model.UserID });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("about", "account", new { @id = userManager.GetUserName(User) });
            }
            else
            {
                return View();
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserID, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("username", model.UserID);
                    char userType = (await userManager.FindByNameAsync(model.UserID)).UserType;
                    if (userType == 'A')
                    {
                        HttpContext.Session.SetString("admin", model.UserID);
                        return RedirectToAction("product", "products");
                    }
                    else
                    {
                        HttpContext.Session.SetString("cust", model.UserID);
                        return RedirectToAction("viewproducts", "customer");
                    }
                        
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }


        public IActionResult About(string id)
        {
            UserDetails user = userRepository.GetUser(id);
            if (user != null)
            {
                if (HttpContext.Session.GetString("username") == id)
                {
                    var model = new UserViewModel
                    {
                        UserID = user.UserID,
                        Name = user.Name,
                        Address = user.Address,
                        Country = user.Country,
                        Pin = user.Pin,
                        Email = user.Email,
                        Phone = user.Phone,
                        Gender = user.Gender,
                        LanguagesKnown = user.LanguagesKnown,
                    };
                    ViewBag.Message = $"USER ID: {model.UserID}";
                    return View(model);
                }
            }

            return RedirectToAction("login");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("admin");
            HttpContext.Session.Remove("cust");
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

    }
}

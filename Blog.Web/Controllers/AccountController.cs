using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = null;
        private readonly SignInManager<IdentityUser> _signINManager = null;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager=userManager;
            _signINManager=signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var loginVM = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
           var signInResult= await _signINManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
            if (signInResult.Succeeded)
            {
                if(!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                
            };
          
           var identityResult= await _userManager.CreateAsync(identityUser, registerViewModel.Password);
            if (identityResult.Succeeded) 
            {
               var roleIdentityResult= await _userManager.AddToRoleAsync(identityUser, "User");
                if (roleIdentityResult.Succeeded) 
                {
                    return RedirectToAction(nameof(Register));
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signINManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

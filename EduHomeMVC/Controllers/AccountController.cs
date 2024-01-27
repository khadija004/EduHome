using Domain.Entities.Common;
using EduHome.ViewModels.Account;
using EduHome.ViewModels.ViewComponentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EduHome.Utilities.Helpers;
using Service.Interfaces;
using System.Threading.Tasks;
using static EduHome.Utilities.Enums.Enum;

namespace EduHome.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            AppUser newUser = new AppUser()
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                UserName = registerVM.UserName,
                Email = registerVM.Email
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, registerVM.Password);
           
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }

            await _userManager.AddToRoleAsync(newUser, UserRoles.Member.ToString());

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var link = Url.Action(nameof(VerifyEmail), "Account", new { userId = newUser.Id, token = code }, Request.Scheme, Request.Host.ToString());
            //string html = $"<a href ={link}>Click here to register</a>";
            //string content = "Registration Confirmation";
            //await _emailService.SendEmailAsync(newUser.Email, newUser.UserName, html, content);
            string html = $"<a href={link}>Click here to reset your password</a><h1>TP225</h1>";

            await _emailService.SendEmailAsync(registerVM.Email, html);

            return RedirectToAction(nameof(EmailVerification));
        }
        public IActionResult EmailVerification()
        {
            return View();
        }
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user is null) return BadRequest();
            await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            AppUser user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);

            if (user is null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
            }

            if (user is null)
            {
                ModelState.AddModelError("", "Email or password did not match with any account");
                return View();
            }

            if (!user.IsActivated)
            {
                ModelState.AddModelError("", "Your account has not been verified yet, please wait for verification or contact the support");
                return View(loginVM);
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            
            if (!signInResult.Succeeded)
            {
                if (signInResult.IsNotAllowed)
                {
                    ModelState.AddModelError("", "To login, first register with your account");
                    return View();
                }
                ModelState.AddModelError("", "Email or password did not match with any account");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        //[Authorize(Roles = "Admin")]
        public async Task CreateRole()
        {
            foreach (var role in System.Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            if (!ModelState.IsValid) return View(forgotPasswordVM);

            var user = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);

            if (user is null)
            {
                //ModelState.AddModelError("", "There is no account associated with this email.");
                //return View(forgotPasswordVM);
                return RedirectToAction(nameof(ForgotPasswordConfirm));
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token = code }, Request.Scheme, Request.Host.ToString());
            string html = $"<a href ={link}>Click here to reset your password</a>";
            await _emailService.SendEmailAsync(user.Email, html);
            return RedirectToAction(nameof(ForgotPasswordConfirm));
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            var resetPasswordModel = new ResetPasswordVM { Email = email, Token = token };
            return View(resetPasswordModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid) return View(resetPasswordVM);

            var user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);

            if (user is null) return NotFound();

            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetPasswordVM.Token, resetPasswordVM.Password);
           
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(resetPasswordVM);
            }
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgotPasswordConfirm()
        {
            return View();
        }

        public async Task<IActionResult> Subscribe(SubscribeVM subscribeVM)
        {
            if (subscribeVM.Email == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    string html = $"<p>You has been subscribed to our newsletter</p>";
                    await _emailService.SendEmailAsync(user.Email, html);
                }
            }
            else
            {
                if (Helpers.IsValidEmail(subscribeVM.Email))
                {
                    var email = subscribeVM.Email;
                    string html = $"<p>You has been subscribed to our newsletter</p>";
                    await _emailService.SendEmailAsync(email, html);
                }
            }
            return Ok();
        }
    }
}

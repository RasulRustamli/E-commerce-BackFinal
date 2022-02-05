using E_commerce_BackFinal.Models;
using E_commerce_BackFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVm register)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = new AppUser
            {
                FullName = register.FullName,
                UserName = register.UserName,
                Email = register.Email,
                Subscribe = register.Subscribe

            };
            //user.IsActive = true;
            IdentityResult identityResult = await _userManager.CreateAsync(user, register.Password);

            if (!identityResult.Succeeded)
            {

                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string link = Url.Action(nameof(VerifyEmail), "Account", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("loremipsump125@gmail.com", "Fiorello");
            mail.To.Add(new MailAddress(user.Email));
            string html = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/template/Email.html"))
            {
                html = reader.ReadToEnd();
            }
            mail.Body = html.Replace("{{link}}", link);
            mail.Subject = "VerifyEmail";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("loremipsump125@gmail.com", "12345@Lm");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(mail);


            //await _userManager.AddToRoleAsync(user, "Member");

            TempData["Success"] = "Please confirm email";

            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> VerifyEmail(string email, string token)
        {

            AppUser user = await _userManager.FindByEmailAsync(email);
            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, true);
            TempData["Success"] = "Email confirmed";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult CheckSignIn()
        {
            return Content(User.Identity.IsAuthenticated.ToString());
        }


        public IActionResult Login()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Login(LoginVm login)
        {
            if (!ModelState.IsValid) return View();
            AppUser dbUser = await _userManager.FindByNameAsync(login.UserName);

            if (dbUser == null)
            {
                ModelState.AddModelError("", "UserName or Password invalid");
                return View();
            }
            if (!dbUser.IsActive)
            {
                ModelState.AddModelError("", "user is deactive");
                return View();
            }
            var singInResult = await _signInManager.PasswordSignInAsync(dbUser, login.Password, true, true);

            if (!dbUser.IsActive)
            {
                ModelState.AddModelError("", "user is deactive");
                return View();
            }

            if (singInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "is lockout");
                return View();
            }
            if (!singInResult.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password invalid");
                return View();
            }

            var roles = await _userManager.GetRolesAsync(dbUser);
            if (roles[0] == "Admin")
            {
                return RedirectToAction("Index", "Dashboard", new { area = "AdminArea" });
            };

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task CreateRole()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }
            if (!await _roleManager.RoleExistsAsync("Member"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
            }

        }





        //public IActionResult ForgetPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public async Task<IActionResult> ForgetPassword(ForgetPassword model)
        //{
        //    AppUser user = await _userManager.FindByEmailAsync(model.User.Email);
        //    if (user == null) return NotFound();

        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //    var link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());

        //    using (MailMessage mail = new MailMessage())
        //    {
        //        mail.From = new MailAddress("loremipsump125@gmail.com", "Reset");
        //        mail.To.Add(user.Email);
        //        mail.Subject = "Reset Password";
        //        mail.Body = $"<a href={link}>Go to Reset Password View</a>";
        //        mail.IsBodyHtml = true;

        //        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
        //        {
        //            smtp.Credentials = new NetworkCredential("loremipsump125@gmail.com", "12345@Lm");
        //            smtp.EnableSsl = true;
        //            smtp.Send(mail);
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        //public async Task<IActionResult> ResetPassword(string email, string token)
        //{
        //    AppUser user = await _userManager.FindByEmailAsync(email);
        //    if (user == null) return NotFound();
        //    ForgetPassword forgetPassword = new ForgetPassword
        //    {
        //        Token = token,
        //        User = user
        //    };
        //    return View(forgetPassword);
        //}
        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //[ActionName("ResetPassword")]
        //public async Task<IActionResult> Reset(ForgetPassword model)
        //{
        //    AppUser user = await _userManager.FindByEmailAsync(model.User.Email);
        //    if (user == null) return NotFound();
        //    ForgetPassword forgetPassword = new ForgetPassword
        //    {
        //        Token = model.Token,
        //        User = user
        //    };

        //    //if (!ModelState.IsValid) return View(forgetPassword);

        //    IdentityResult result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);


        //    foreach (var item in result.Errors)
        //    {
        //        ModelState.AddModelError("", item.Description);
        //        return View(forgetPassword);
        //    }

        //    return RedirectToAction("Index", "Home");

        //}
    }
    }

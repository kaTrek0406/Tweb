using System;

using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using JW.BusinessLogic.Services;
using JW.Domain;
using JW.Infrastructure;

namespace JW.WebApi.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly AuthService _authManager = new AuthService();

        // GET: Login page
        public ActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Login process
        [HttpPost]
        public ActionResult SignIn(string username, string password)
        {
            var user = _authManager.AuthenticateUser(username, password);
            if (user != null)
            {
                AuthenticateAndRedirect(user);
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.ErrorMessage = "Неверное имя пользователя или пароль";
            return View();
        }

        // GET: Register page
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Register process
        [HttpPost]
        public ActionResult SignUp(string username, string password)
        {
            var user = _authManager.CreateAccount(username, password);
            if (user != null)
            {
                AuthenticateAndRedirect(user);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Ошибка при регистрации пользователя";
            return View();
        }

        // Log out current user
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            HttpContext.Response.Cookies.Add(authCookie);

            return RedirectToAction("SignIn");
        }

        // Helper method to handle user authentication
        private void AuthenticateAndRedirect(Customer user)
        {
            var ticket = new FormsAuthenticationTicket(
                1, // ticket version
                user.Username,
                DateTime.Now,
                DateTime.Now.AddMinutes(20), // expiration
                true, // persistent cookie
                user.Role, // user data
                "/");

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(cookie);
        }
    }
}
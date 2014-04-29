using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Geekhub.App.Modules.Users.Queries;
using Geekhub.App.Modules.Users.Data;

namespace Geekhub.App.Controllers
{
    public class UsersController : Controller
    {
        private UsersService _usersService = new UsersService();

        [Route("users/login")]
        public ActionResult Login(string returnUrl)
        {
            if (returnUrl != null && returnUrl.EndsWith("/meetings/create")) {
                ViewBag.Scenario = "create";
            }

            return View();
        }

        [HttpPost]
        [Route("users/login")]
        public ActionResult Login(string email, string name, string returnUrl)
        {
            var user = new FetchUserByEmailQuery(email).User;

            if (user == null && string.IsNullOrWhiteSpace(name)) {
                ViewBag.Email = email;
                return View("Create");
            }

            if (user == null && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(name)) {
                _usersService.CreateUser(email, name);
            }

            _usersService.SendUserLoginEmail(email);
            
            return RedirectToAction("Validate", new { email, returnUrl});

        }

        [Route("users/validate")]
        public ActionResult Validate(string email, string code = "", string returnUrl = "")
        {
            if (!string.IsNullOrWhiteSpace(code)) {
                if(new IsUserValidationCodeValidQuery(email, code).IsValid)
                {
                    _usersService.ExpireUserValidationCode(email);
                    
                    //create a new forms auth ticket
                    var ticket = new FormsAuthenticationTicket(2,
                        email, DateTime.Now, DateTime.Now.AddYears(1), true, String.Empty);
                    //encrypt the ticket
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    var authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) {
                        Expires = ticket.Expiration
                    };

                    Response.Cookies.Add(authenticationCookie);

                    if (!string.IsNullOrWhiteSpace(returnUrl)) {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Meetings");
                } else {
                    _usersService.RegisterInvalidValidationCode(email);
                }

            }

            return View();
        }

        [Route("users/logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Meetings");
        }
    }
}
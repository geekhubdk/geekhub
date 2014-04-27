using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Modules.Users.Commands;
using Geekhub.App.Modules.Users.Queries;

namespace Geekhub.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly CommandBus _commandBus;
        private readonly FetchUserByEmailQuery _fetchUserByEmailQuery;
        private readonly IsUserValidationCodeValidQuery _isUserValidationCodeValidQuery;

        public UsersController(CommandBus commandBus, FetchUserByEmailQuery fetchUserByEmailQuery, IsUserValidationCodeValidQuery isUserValidationCodeValidQuery)
        {
            _commandBus = commandBus;
            _fetchUserByEmailQuery = fetchUserByEmailQuery;
            _isUserValidationCodeValidQuery = isUserValidationCodeValidQuery;
        }

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
                _commandBus.Execute(new CreateUserCommand(email, name));
            }

            _commandBus.Execute(new SendUserLoginEmailCommand(email));
            
            return RedirectToAction("Validate", new { email, returnUrl});

        }

        [Route("users/validate")]
        public ActionResult Validate(string email, string code = "", string returnUrl = "")
        {
            if (!string.IsNullOrWhiteSpace(code)) {
                if(new IsUserValidationCodeValidQuery(email, code).IsValid)
                {
                    _commandBus.Execute(new ExpireUserValidationCodeCommand(email));
                    
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
                    _commandBus.Execute(new RegisterInvalidValidationCodeCommand(email));
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
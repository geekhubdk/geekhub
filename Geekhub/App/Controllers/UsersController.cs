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
            var user = _fetchUserByEmailQuery.Execute(email);

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
            if (!string.IsNullOrWhiteSpace(code) && _isUserValidationCodeValidQuery.Execute(email, code)) {
                _commandBus.Execute(new ExpireUserValidationCodeCommand(email));
                FormsAuthentication.SetAuthCookie(email.ToLower(), true);

                if (!string.IsNullOrWhiteSpace(returnUrl)) {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Meetings");
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
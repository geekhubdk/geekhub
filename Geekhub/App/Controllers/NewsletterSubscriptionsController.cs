using System.Web.Mvc;
using Geekhub.App.Modules.Alerts.Support;

namespace Geekhub.App.Controllers
{
    public class NewsletterSubscriptionsController : ControllerBase
    {
        [Route("newsletter/subscribe")]
        public ActionResult Create()
        {
            IncludeSubscriberCount();
            return View();
        }

        [Route("newsletter/subscribe")]
        [HttpPost]
        public ActionResult Create(string email)
        {
            IncludeSubscriberCount();
            if (string.IsNullOrEmpty(email)) {
                return View();
            }

            AlertsService.SubscribeToNewsletter(email);

            Notice("Du er tilmeldt nyhedsbrevet");
            return Redirect("~/");
        }

        [Route("newsletter/unsubscribe")]
        public ActionResult Delete()
        {
            return View();
        }

        [Route("newsletter/unsubscribe")]
        [HttpPost]
        public ActionResult Delete(string email)
        {
            if (string.IsNullOrEmpty(email)) {
                return View();
            }

            try {
                AlertsService.UnsubscribeFromNewsletter(email);
            } catch (SubscriptionNotFoundException) {
                Warn("Vi kunne ikke finde dig. Prøv en anden email");
                return View();
            }
            
            Notice("Du er nu afmeldt nyhedsbreve");

            return Redirect("~/");
        }
        
        private void IncludeSubscriberCount()
        {
            ViewBag.SubscriberCount = AlertsRepository.GetNewsletterSubscriberCount();
        }

	}
}
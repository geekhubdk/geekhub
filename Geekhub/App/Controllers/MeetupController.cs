using System.Web.Mvc;
using Geekhub.App.Modules.Meetings.MeetingProviders;

namespace Geekhub.App.Controllers
{
    public class MeetupController : Controller
    {
        [Route("meetup/pullgroup")]
        public ActionResult PullGroup(string group)
        {
            var provider = new MeetingProviderCoordinator();
            return Json(provider.PullMeetings(), JsonRequestBehavior.AllowGet);
        }

    }
}
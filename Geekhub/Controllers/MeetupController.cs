using System.Web.Mvc;
using Geekhub.Modules.Meetings.MeetingProviders;

namespace Geekhub.Controllers
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
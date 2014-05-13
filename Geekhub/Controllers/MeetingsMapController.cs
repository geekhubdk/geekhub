using System.Web.Mvc;
using Geekhub.Modules.Meetings.Data;
using Geekhub.Modules.Meetings.Support;

namespace Geekhub.Controllers
{
    public class MeetingsMapController : Controller
    {
        [Route("Map")]
        public ActionResult Index()
        {
            var meetings = new MeetingsRepository().GetUpcommingMeetings();

            ViewData.Model = MeetingsMapHelper.CreateMeetingMapModel(meetings, Url);

            return View();
        }
    }
}
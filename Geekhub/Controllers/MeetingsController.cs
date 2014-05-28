using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Deldysoft.Foundation;
using Geekhub.Modules.Core.Mvc;
using Geekhub.Modules.Core.Support;
using Geekhub.Modules.Meetings.Support;
using Geekhub.Modules.Meetings.ViewModels;
using Newtonsoft.Json;

namespace Geekhub.Controllers
{
    public class MeetingsController : Modules.Core.Mvc.ControllerBase
    {
        [Route("meetings")]
        public ActionResult Index()
        {
            ViewBag.MetaDescription =
                "Geekhub.dk er stedet hvor udviklere finder deres events/arrangementer - foreslå dit event til listen, og spred budskabet.";

            var upcommingMeetings = MeetingsRepository.GetUpcommingMeetings(Request.QueryString);
            return View(upcommingMeetings);
        }

        [Route("arkiv")]
        public ActionResult Archive()
        {
            var archivedMeetings = MeetingsRepository.GetArchivedMeetings(Request.QueryString);
            return View(archivedMeetings);
        }

        [Route("meetings/{id}")]
        public ActionResult Show(int id)
        {
            var meeting = MeetingsRepository.GetMeeting(id);
            return View(meeting);
        }

        [Authorize]
        [Route("meetings/create")]
        public ActionResult Create()
        {
            LoadFormData();
            var formModel = new MeetingFormModel();
            return View(formModel);
        }

        [HttpPost]
        [Authorize]
        [Route("meetings/create")]
        public ActionResult Create(MeetingFormModel formModel)
        {
            LoadFormData();

            if (ModelState.IsValid) {
                var meeting = MeetingsService.Create(formModel);
                Notice("Se dit flotte møde :)");
                return RedirectToAction("Show", new { id = meeting.Id });
            }

            return View(formModel);
        }

        [HttpPost]
        [Authorize]
        [Route("meetings/{id}/delete")]
        public ActionResult Delete(int id)
        {
            MeetingsService.Delete(id);

            Notice("Du føler dig destruktiv idag, hva? Pyt. Mødet er dæbt.");

            return RedirectToAction("Index");
        }

        [Authorize]
        [Route("meetings/{id}/edit")]
        public ActionResult Edit(int id)
        {
            LoadFormData();

            var meeting = MeetingsRepository.GetMeeting(id);
            var formModel = new MeetingFormModel(meeting);

            return View("Create", formModel);
        }

        [HttpPost]
        [Authorize]
        [Route("meetings/{id}/edit")]
        public ActionResult Edit(int id, MeetingFormModel formModel)
        {
            LoadFormData();

            if (ModelState.IsValid) {
                MeetingsService.Save(id, formModel);
                Notice("Se dit flotte møde :)");
                return RedirectToAction("Show", new { id });
            }

            return View("Create", formModel);
        }

        [Route("meetings.rss")]
        public ActionResult Rss()
        {
            var model = MeetingsRepository.GetUpcommingMeetings(Request.QueryString);
            return PartialView(new RssViewModel(model));
        }

        [Route("meetings.json")]
        public ActionResult Json()
        {
            var model = MeetingsRepository.GetUpcommingMeetings(Request.QueryString);
            return Json(new JsonViewModel(model), JsonRequestBehavior.AllowGet);
        }

        [Route("widget")]
        public ActionResult Widget()
        {
            var title = Request.QueryString["title"].Or("Udvikler events i danmark");
            var meetings = MeetingsRepository.GetUpcommingMeetings(Request.QueryString);

            var model = new WidgetViewModel(title, meetings.Take(5));
            return PartialView(model);
        }

        [Route("meetings.ics")]
        public ActionResult Ics()
        {
            var meetings = MeetingsRepository.GetUpcommingMeetings(Request.QueryString);
            return new CalendarResult("Geekhub", meetings.Select(x => new CalendarResult.Event() {
                DateStart = x.StartsAt,
                DateEnd = x.StartsAt.AddHours(2),
                Description = x.Description,
                Summary = x.Title,
                Url = MeetingUrlGenerator.CreateFullMeetingUrl(x.Id, "ics"),
            }));
        }

        [Route("meetings/pull")]
        public ActionResult Pull()
        {
            if (AppEnvironment.Current != EnvironmentType.Development) {
                return Content("Only allowed in development mode");
            }
            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var result = client.DownloadString("http://www.geekhub.dk/meetings.json?ticks=" + DateTime.Now.Ticks);
            var json = JsonConvert.DeserializeObject<JsonViewModel>(result);

            MeetingsService.DeleteAll();

            foreach (var meeting in json.Items) {
                MeetingsService.Create(meeting);
            }

            return Content("Completed");
        }

        [Route("Partials/MeetingsForFrontpage")]
        public ActionResult MeetingsForFrontpage()
        {
            var upcommingMeetings = MeetingsRepository.GetUpcommingMeetings(Request.QueryString).ToArray();
            ViewBag.MapData = MeetingsMapHelper.CreateMeetingMapModel(upcommingMeetings, Url);
            return PartialView(upcommingMeetings.Take(3));
        }

        private void LoadFormData()
        {
            ViewBag.Organizers = JsonConvert.SerializeObject(MeetingsRepository.GetMeetingOrganizers());
            ViewBag.Tags = JsonConvert.SerializeObject(MeetingsRepository.GetMeetingsTags());
        }
    }
}
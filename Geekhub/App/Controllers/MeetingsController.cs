using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Deldysoft.Foundation;


using Geekhub.App.Core.Data;
using Geekhub.App.Core.Mvc;
using Geekhub.App.Core.Support;
using Geekhub.App.Modules.Meetings.Models;
using Geekhub.App.Modules.Meetings.Queries;
using Geekhub.App.Modules.Meetings.Support;
using Geekhub.App.Modules.Meetings.ViewModels;
using Newtonsoft.Json;
using Geekhub.App.Modules.Meetings.CommandHandlers;

namespace Geekhub.App.Controllers
{
    public class MeetingsController : ControllerBase
    {
        [Route("meetings")]
        public ActionResult Index()
        {
            ViewBag.MetaDescription =
                "Geekhub.dk er stedet hvor udviklere finder deres events/arrangementer - foreslå dit event til listen, og spred budskabet.";

            var upcommingMeetings = new UpcommingMeetingsQuery(Request.QueryString).Meetings;
            return View(upcommingMeetings);
        }

        [Route("arkiv")]
        public ActionResult Archive()
        {
            var archivedMeetings = new ArchivedMeetingsQuery().Meetings;
            return View(archivedMeetings);
        }

        [Route("meetings/{id}")]
        public ActionResult Show(int id)
        {
            var meeting = new ShowMeetingQuery(id).Meeting;
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
                new CreateMeetingCommandHandler(formModel);
                Notice("Se dit flotte møde :)");
                return RedirectToAction("Index");
            }

            return View(formModel);
        }

        [HttpPost]
        [Authorize]
        [Route("meetings/{id}/delete")]
        public ActionResult Delete(int id)
        {
            new DeleteMeetingCommandHandler(id);

            Notice("Du føler dig destruktiv idag, hva? Pyt. Mødet er dæbt.");

            return RedirectToAction("Index");
        }

        [Authorize]
        [Route("meetings/{id}/edit")]
        public ActionResult Edit(int id)
        {
            LoadFormData();

            var meeting = new ShowMeetingQuery(id).Meeting;
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
                new SaveMeetingCommandHandler(id, formModel);
                Notice("Se dit flotte møde :)");
                return RedirectToAction("Show", new { id });
            }

            return View("Create", formModel);
        }

        [Route("meetings.rss")]
        public ActionResult Rss()
        {
            var model = new UpcommingMeetingsQuery(Request.QueryString).Meetings;
            return PartialView(new RssViewModel(model));
        }

        [Route("meetings.json")]
        public ActionResult Json()
        {
            var model = new UpcommingMeetingsQuery(Request.QueryString).Meetings;
            return Json(new JsonViewModel(model), JsonRequestBehavior.AllowGet);
        }

        [Route("widget")]
        public ActionResult Widget()
        {
            var title = Request.QueryString["title"].Or("Udvikler events i danmark");
            var meetings = new UpcommingMeetingsQuery(Request.QueryString).Meetings;

            var model = new WidgetViewModel(title, meetings.Take(5));
            return PartialView(model);
        }

        [Route("meetings.ics")]
        public ActionResult Ics()
        {
            var meetings = new UpcommingMeetingsQuery(Request.QueryString).Meetings;
            return new CalendarResult("Geekhub", meetings.Select(x=>new CalendarResult.Event() {
                    DateStart = x.StartsAt,
                    DateEnd = x.StartsAt.AddHours(2),
                    Description = x.Description,
                    Summary = x.Title,
                    Url = MeetingUrlGenerator.CreateFullMeetingUrl(x.Id,"ics"),
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

            new DeleteAllMeetingsCommandHandler().Execute();

            foreach (var meeting in json.Items) {
                new CreateMeetingCommandHandler(meeting);
            }

            return Content("Completed");
        }

        [Route("Partials/MeetingsForFrontpage")]
        public ActionResult MeetingsForFrontpage()
        {
            var upcommingMeetings = new UpcommingMeetingsQuery(Request.QueryString).Meetings.Take(3);
            return PartialView(upcommingMeetings);
        }

        private void LoadFormData()
        {
            var query = new LoadMeetingFormDataQuery();
            ViewBag.Organizers = JsonConvert.SerializeObject(query.Organizers);
            ViewBag.Tags = JsonConvert.SerializeObject(query.Tags);
        }
    }
}
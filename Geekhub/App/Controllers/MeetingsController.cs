using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Deldysoft.Foundation;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Core.Mvc;
using Geekhub.App.Core.Support;
using Geekhub.App.Modules.Meetings.Commands;
using Geekhub.App.Modules.Meetings.Models;
using Geekhub.App.Modules.Meetings.Queries;
using Geekhub.App.Modules.Meetings.Support;
using Geekhub.App.Modules.Meetings.ViewModels;
using Newtonsoft.Json;

namespace Geekhub.App.Controllers
{
    public class MeetingsController : ControllerBase
    {
        private readonly ICommandExecuter _commandExecuter;
        private readonly UpcommingMeetingsQuery _upcommingMeetingsQuery;
        private readonly ArchivedMeetingsQuery _archivedMeetingsQuery;
        private readonly ShowMeetingQuery _showMeetingQuery;
        private readonly LoadMeetingFormDataQuery _loadMeetingFormDataQuery;

        public MeetingsController(ICommandExecuter commandExecuter, UpcommingMeetingsQuery upcommingMeetingsQuery, ShowMeetingQuery showMeetingQuery, LoadMeetingFormDataQuery loadMeetingFormDataQuery, ArchivedMeetingsQuery archivedMeetingsQuery)
        {
            _commandExecuter = commandExecuter;
            _upcommingMeetingsQuery = upcommingMeetingsQuery;
            _showMeetingQuery = showMeetingQuery;
            _loadMeetingFormDataQuery = loadMeetingFormDataQuery;
            _archivedMeetingsQuery = archivedMeetingsQuery;
        }

        [Route("")]
        public ActionResult Index()
        {
            ViewBag.MetaDescription =
                "Geekhub.dk er stedet hvor udviklere finder deres events/arrangementer - foreslå dit event til listen, og spred budskabet.";

            var upcommingMeetings = _upcommingMeetingsQuery.Execute(Request.QueryString);
            return View(upcommingMeetings);
        }

        [Route("arkiv")]
        public ActionResult Archive()
        {
            var archivedMeetings = _archivedMeetingsQuery.Execute();
            return View(archivedMeetings);
        }

        [Route("meetings/{id}")]
        public ActionResult Show(int id)
        {
            var meeting = _showMeetingQuery.Execute(id);
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
                _commandExecuter.Execute(new CreateMeetingCommand(formModel, User.Identity.Name));
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
            _commandExecuter.Execute(new DeleteMeetingCommand(id, User.Identity.Name));

            Notice("Du føler dig destruktiv idag, hva? Pyt. Mødet er dæbt.");

            return RedirectToAction("Index");
        }

        [Authorize]
        [Route("meetings/{id}/edit")]
        public ActionResult Edit(int id)
        {
            LoadFormData();

            var meeting = _showMeetingQuery.Execute(id);
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
                _commandExecuter.Execute(new SaveMeetingCommand(id, User.Identity.Name, formModel));
                Notice("Se dit flotte møde :)");
                return RedirectToAction("Show", new { id });
            }

            return View("Create", formModel);
        }

        [Route("meetings.rss")]
        public ActionResult Rss()
        {
            var model = _upcommingMeetingsQuery.Execute(Request.QueryString);
            return PartialView(new RssViewModel(model));
        }

        [Route("meetings.json")]
        public ActionResult Json()
        {
            var model = _upcommingMeetingsQuery.Execute(Request.QueryString);
            return Json(new JsonViewModel(model), JsonRequestBehavior.AllowGet);
        }

        [Route("widget")]
        public ActionResult Widget()
        {
            var title = Request.QueryString["title"].Or("Udvikler events i danmark");
            var meetings = _upcommingMeetingsQuery.Execute(Request.QueryString);

            var model = new WidgetViewModel(title, meetings.Take(5));
            return PartialView(model);
        }

        [Route("meetings.ics")]
        public ActionResult Ics()
        {
            var meetings = _upcommingMeetingsQuery.Execute(Request.QueryString);
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

            var deleteAll = new DeleteAllMeetingsCommand();
            _commandExecuter.Execute(deleteAll);

            foreach (var meeting in json.Items) {
                var cmd = new CreateMeetingFromJsonCommand(meeting, "Pull");
                _commandExecuter.Execute(cmd);
            }

            return Content("Completed");
        }

        private void LoadFormData()
        {
            var result = _loadMeetingFormDataQuery.Execute();
            ViewBag.Organizers = JsonConvert.SerializeObject(result.Organizers);
            ViewBag.Tags = JsonConvert.SerializeObject(result.Tags);
        }

    }
}
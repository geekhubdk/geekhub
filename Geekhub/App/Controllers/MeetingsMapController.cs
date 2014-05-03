using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Geekhub.App.Controllers
{
    public class MeetingsMapController : Controller
    {
        [Route("Map")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
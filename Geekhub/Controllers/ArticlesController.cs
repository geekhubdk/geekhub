using System.Web.Mvc;

namespace Geekhub.Controllers
{
    [Route("articles/{action}")]
    public class ArticlesController : Controller
    {
        public ActionResult Guidelines()
        {
            return View();
        }

        public ActionResult Embed()
        {
            return View();
        }

        [Route("om")]
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Rss()
        {
            return View();
        }

        public ActionResult Ics()
        {
            return View();
        }
	}
}
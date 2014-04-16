using System.Web.Mvc;

namespace Geekhub.App.Controllers
{
    public class ArticlesController : Controller
    {
        [Route("articles/guidelines")]
        public ActionResult Guidelines()
        {
            return View();
        }

        [Route("articles/embed")]
        public ActionResult Embed()
        {
            return View();
        }

        [Route("om")]
        public ActionResult About()
        {
            return View();
        }

        [Route("articles/rss")]
        public ActionResult Rss()
        {
            return View();
        }

        [Route("articles/ics")]
        public ActionResult Ics()
        {
            return View();
        }
	}
}
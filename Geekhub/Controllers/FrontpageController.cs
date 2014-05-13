using System.Web.Mvc;

namespace Geekhub.Controllers
{
    public class FrontpageController : Controller
    {
        // GET: Frontpage
        [Route("")]
        public ActionResult Index()
        {
            ViewBag.MetaDescription =
                "Geekhub.dk er stedet hvor udviklere finder deres events/arrangementer - foreslå dit event til listen, og spred budskabet.";
            return View();
        }
    }
}
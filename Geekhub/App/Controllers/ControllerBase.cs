using System.Web.Mvc;

namespace Geekhub.App.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected void Notice(string message)
        {
            TempData["notice"] = message;
        }

        protected void Warn(string message)
        {
            TempData["warn"] = message;
        }
    }
}
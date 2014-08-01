using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klepto;
using Klepto.EventProviders;

namespace Geekhub.Controllers
{
    public class KleptoController : Controller
    {
        [Route("klepto")]
        public ActionResult GetInformation(string url)
        {
            string clientID = ConfigurationManager.AppSettings["Facebook.ClientID"];
            string secret = ConfigurationManager.AppSettings["Facebook.ClientSecret"];

            var instance = Kleptomanic.RegisterSchema<EventInformation>()
                .AddProvider(new TriforkProvider())
                .AddProvider(new ProsaProvider())
                .AddProvider(new MeetupProvider())
                .AddProvider(new IdaProvider());

            if(!string.IsNullOrWhiteSpace(clientID) && !string.IsNullOrWhiteSpace(secret)) {
                instance.AddProvider(new FacebookProvider(clientID, secret));
            }

            return Json(instance.GetResult(url), JsonRequestBehavior.AllowGet);
        }
    }
}
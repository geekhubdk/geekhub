using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deldysoft.Foundation;

namespace Geekhub.Modules.Core.Mvc
{
    public class OnlyDevelopmentAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AppEnvironment.Current != EnvironmentType.Development) {
                throw new Exception("Only allowed in development mode");
            }
        }
    }
}
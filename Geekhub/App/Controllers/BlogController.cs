using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Geekhub.App.Modules.Blog.Support;

namespace Geekhub.App.Controllers
{
    public class BlogController : Controller
    {
        private BlogRssPostReader _blogRssPostReader = new BlogRssPostReader();

        [Route("Partials/BlogPostsForFrontpage")]
        public ActionResult BlogPostsForFrontpage()
        {
            try {
                return PartialView(_blogRssPostReader.GetLatestPosts(3));
            } catch (Exception ex) {
                return Content("Could not load view: " + ex.Message);
            }
        }

    }
}
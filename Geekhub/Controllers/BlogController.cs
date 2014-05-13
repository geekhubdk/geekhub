using System;
using System.Web.Mvc;
using Geekhub.Modules.Blog.Support;

namespace Geekhub.Controllers
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
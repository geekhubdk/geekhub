using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geekhub.App.Modules.Blog.ViewModels
{
    public class BlogPostViewModel
    {
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Link { get; set; }
    }
}
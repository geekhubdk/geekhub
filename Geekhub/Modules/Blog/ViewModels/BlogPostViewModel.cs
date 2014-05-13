using System;

namespace Geekhub.Modules.Blog.ViewModels
{
    public class BlogPostViewModel
    {
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Link { get; set; }
    }
}
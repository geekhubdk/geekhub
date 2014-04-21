using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Geekhub.App.Modules.Blog.ViewModels;

namespace Geekhub.App.Modules.Blog.Support
{
    public class BlogRssPostReader
    {
        public BlogPostViewModel[] GetLatestPosts(int count) {
            var reader = XmlReader.Create("https://medium.com/feed/@geekhubdk/");
            var feed = SyndicationFeed.Load(reader);
            reader.Close();

            if (feed != null) {
                return feed.Items.Take(3).Select(x => new BlogPostViewModel() { Title = x.Title.Text, CreatedAt = x.PublishDate.DateTime, Link = x.Links.First().Uri.ToString() }).ToArray();
            } else {
                return new BlogPostViewModel[0];
            }
        }
    }
}
using Geekhub.Modules.Meetings.Models;

namespace Geekhub.Modules.Alerts.Support
{
    public class TwitterMeetingAlertGenerator
    {
        private const int TweetLength = 120;

        public string GenerateMessage(Meeting meeting)
        {
            var message = meeting.Title;
            var hashTags = "#" + meeting.City.Name.ToLower();
            var link = "geekhub.dk/m/" + meeting.Id;
            var date = meeting.StartsAt.ToString("d. MMM");
            return SendTweetWithLink(message, hashTags, link, date);
        }

        public string SendTweetWithLink(string message, string hashTags, string link, string date)
        {
            int charsLeft = TweetLength - hashTags.Length - link.Length - date.Length - 5;

            if (message.Length > charsLeft) {
                message = message.Substring(charsLeft - 3) + "...";
            }

            return string.Concat(message, " ", "(", date, ") ", hashTags, " ", link);
        }

    }
}
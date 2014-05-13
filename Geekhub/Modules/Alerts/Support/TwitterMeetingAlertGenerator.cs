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
            return SendTweetWithLink(message, hashTags , link);
        }

        public string SendTweetWithLink(string message, string hashTags, string link)
        {
            const string delimiter = " ";

            int charsLeft = TweetLength - hashTags.Length - link.Length - delimiter.Length*2;

            if (message.Length > charsLeft) {
                message = message.Substring(charsLeft - 3) + "...";
            }

            return string.Concat(message, delimiter, hashTags, delimiter, link);
        }

    }
}
using System.Web;

namespace Geekhub.App.Modules.Meetings.Support
{
    public static class MeetingUrlGenerator
    {
        public static string CreateFullMeetingUrl(int id, string googleAnalyticsSource)
        {
            var baseUrl = string.Format("http://geekhub.dk/meetings/{0}", id);

            var parameters = HttpUtility.ParseQueryString(string.Empty);

            if (!string.IsNullOrEmpty(googleAnalyticsSource)) {
                parameters.Add("utm_source", googleAnalyticsSource);
            }

            if (parameters.Count > 0) {
                return baseUrl + "?" + parameters;
            }
            return baseUrl;
        }
    }
}
using System;
using System.Diagnostics;
using System.Linq;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Alerts.Adapters;
using Geekhub.App.Modules.Alerts.Models;
using Geekhub.App.Modules.Alerts.Support;
using Geekhub.App.Modules.Alerts.Config;
using Geekhub.App.Modules.Meetings.Data;

namespace Geekhub.App.Modules.Alerts.CommandHandlers
{
    public class SendTweetsForMeetingsCommandHandler
    {
        private readonly TwitterMeetingAlertGenerator _twitterMeetingAlertGenerator = new TwitterMeetingAlertGenerator();
        private readonly ITwitterAdapter _twitterAdapter = AlertsContainerConfig.CreateTwitterAdapter();

        private readonly MeetingsRepository _meetingsRepository = new MeetingsRepository();

        public SendTweetsForMeetingsCommandHandler(TimeSpan waitTime)
        {
            var meetings = _meetingsRepository.GetUpcommingMeetings();
            foreach (var meeting in meetings) {

                // We will wait for 1 hour before sending tweets
                if (DateTime.Now - meeting.CreatedAt < waitTime) {
                    continue;
                }

                if (!DataContext.Current.MeetingTweetAlerts.Any(x => x.MeetingId == meeting.Id)) {
                    var alert = new MeetingTweetAlert();
                    alert.Message = _twitterMeetingAlertGenerator.GenerateMessage(meeting);
                    alert.DateSent = DateTime.Now;
                    alert.MeetingId = meeting.Id;

                    try {
                        _twitterAdapter.SendTweet(alert.Message);
                    } catch (Exception ex) {
                        alert.DateFailed = DateTime.Now;
                        alert.FailMessage = ex.Message;
                        Debug.WriteLine("Could not send twitter message for meeting {0}: {1}", meeting.Id, ex.Message);
                    }

                    DataContext.Current.MeetingTweetAlerts.Add(alert);
                }
            }
        }
    }
}
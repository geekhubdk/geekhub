using System;
using System.Diagnostics;
using System.Linq;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Alerts.Adapters;
using Geekhub.App.Modules.Alerts.Commands;
using Geekhub.App.Modules.Alerts.Models;
using Geekhub.App.Modules.Alerts.Support;
using Geekhub.App.Modules.Meetings.Queries;

namespace Geekhub.App.Modules.Alerts.CommandHandlers
{
    public class SendTweetsForMeetingsCommandHandler : CommandHandlerBase, IHandleCommand<SendTweetsForMeetingsCommand>
    {
        private readonly TwitterMeetingAlertGenerator _twitterMeetingAlertGenerator;
        private readonly ITwitterAdapter _twitterAdapter;
        private readonly UpcommingMeetingsQuery _upcommingMeetingsQuery;

        public SendTweetsForMeetingsCommandHandler(DataContext dataContext, TwitterMeetingAlertGenerator twitterMeetingAlertGenerator, ITwitterAdapter twitterAdapter, UpcommingMeetingsQuery upcommingMeetingsQuery) : base(dataContext)
        {
            _twitterMeetingAlertGenerator = twitterMeetingAlertGenerator;
            _twitterAdapter = twitterAdapter;
            _upcommingMeetingsQuery = upcommingMeetingsQuery;
        }

        public void Execute(SendTweetsForMeetingsCommand command)
        {
            foreach (var meeting in _upcommingMeetingsQuery.Execute()) {

                // We will wait for 1 hour before sending tweets
                if (DateTime.Now - meeting.CreatedAt < command.WaitTime) {
                    continue;
                }

                if (!DataContext.MeetingTweetAlerts.Any(x => x.MeetingId == meeting.Id)) {
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

                    DataContext.MeetingTweetAlerts.Add(alert);
                }
            }
        }
    }
}
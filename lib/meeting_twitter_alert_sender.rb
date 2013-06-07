class MeetingTwitterAlertSender
  def run
    Meeting.available_for_tweet_alerts.each do |m|
      Twitter.update(m.title)
      m.meeting_tweet_alerts.create
    end
  end
end
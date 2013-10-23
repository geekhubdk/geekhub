class MeetingTwitterAlertSender
  def run
    Meeting.available_for_tweet_alerts.each do |m|
      message = self.message_for(m)
      puts "Sending message: #{message}"
      Twitter.update(message)
      m.meeting_tweet_alerts.create
    end
  end

  def message_for meeting
    title = "#{meeting.title}"
    url = "##{tagify(meeting.city.name)} - geekhub.dk/m/#{meeting.id}"
    title_max_length = 140 - url.length - 3
    if title.length > title_max_length
      title = title.strip.slice(0, title_max_length-3) + '...'
    end
    "#{title} - #{url}"
  end

  def tagify text
    text.downcase.gsub(" ", "")
  end
end
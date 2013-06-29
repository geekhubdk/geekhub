require 'active_support/inflections'
require 'action_view/helpers'
extend ActionView::Helpers

desc 'Send alerts of new meetings'
task :alerts => :environment do
  puts 'Sending alerts'

  puts '- Finding meetings that is available for alerts'

  meetings = Meeting.available_for_alerts

  puts "- Found #{pluralize(meetings.count, 'meeting')} to send alerts for"

  if meetings.length > 0
    puts '- Finding subscriptions'

    subscriptions = MeetingEmailAlertSubscription.all

    puts "- Found #{pluralize(subscriptions.count, 'subscription')}"

    emails = subscriptions.map{|s| s.user.email}

    meetings.each do |m|
      m.meeting_email_alerts.create
    end

    emails.each do |e|
      puts "- Sending mail to: #{e}"
      MeetingAlertMailer.alert_email(meetings, e).deliver
    end

    meetings.each do |m|
      twitter_update = "#{m.title}"
      puts "Sending update: #{twitter_update}"
    end

    begin
      Twitter.update(twitter_update)
    rescue e
      error_message="#{$!}"
      puts "Could not send tweet: #{error_message}"
    end
  else
    puts '- No alerts to send'
  end
end
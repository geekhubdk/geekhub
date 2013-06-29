require 'active_support/inflections'
require 'action_view/helpers'
require 'meeting_twitter_alert_sender'
extend ActionView::Helpers

desc 'Send twitter alerts of new meetings'
task :twitter_alerts => :environment do
  puts 'Sending alerts'
  MeetingTwitterAlertSender.new.run
end
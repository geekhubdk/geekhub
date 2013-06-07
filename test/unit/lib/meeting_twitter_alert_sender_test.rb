require 'test_helper'
require 'meeting_twitter_alert_sender'
class MeetingTwitterAlertSenderTest < ActiveSupport::TestCase
  test "Check currect output" do
    sender = MeetingTwitterAlertSender.new
    assert_equal "blah - geekhub.dk/m/10", sender.message_for(10, 'blah')
  end

  test "Check max length" do
    sender = MeetingTwitterAlertSender.new
    long_title = ''
    (0..20).each do
      long_title += '1234567890'
    end

    assert_equal 140, sender.message_for(10, long_title).length
  end
end

require 'test_helper'
require 'meeting_twitter_alert_sender'
class MeetingTwitterAlertSenderTest < ActiveSupport::TestCase
  test 'Check currect output' do
    sender = MeetingTwitterAlertSender.new
    m = Meeting.new
    m.id = 10
    m.title = 'blah'
    m.city = City.new
    m.city.name = "City"
    
    assert_equal 'blah - #city - geekhub.dk/m/10', sender.message_for(m)
  end

  test 'Check max length' do
    sender = MeetingTwitterAlertSender.new
    long_title = ''
    (0..20).each do
      long_title += '1234567890'
    end
    
    m = Meeting.new
    m.id = 10
    m.title = long_title
    m.city = City.new
    m.city.name = "City"

    assert_equal 140, sender.message_for(m).length
  end
end

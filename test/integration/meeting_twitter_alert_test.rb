require 'test_helper'
require 'meeting_twitter_alert_sender'
class MeetingTwitterAlertTest < ActionDispatch::IntegrationTest
  test 'sending a tweet for a new meeting' do
    random_meeting_name = SecureRandom.hex
    
    clear_meetings
    new_upcomming_meeting random_meeting_name
    run_twitter_task
    run_twitter_task
    assert_tweet_created random_meeting_name
  end

  def clear_meetings
    Meeting.destroy_all
  end

  def run_twitter_task
    MeetingTwitterAlertSender.new().run()
  end

  def new_upcomming_meeting name
    Meeting.create({
        id: 1,
        title: name,
        starts_at: Time.now + 2.days,
        city_id: 1,
        organizer: 'ONUG',
        description: 'This is a description',
        url: 'http://mysite.dk',
        approved_at: '2012-01-06 13:59:34',
        created_at: '2012-01-06 13:59:34',
        user_id: 1
    })
  end

  def assert_tweet_created message
    timeline = Twitter.user_timeline('geekhubdk_test')
    matching_tweets = timeline.select{|t| t.text.include? message}
    number_of_tweets_with_message = matching_tweets.length
    assert_equal 1, number_of_tweets_with_message
  end

end

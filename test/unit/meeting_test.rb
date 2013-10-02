require 'test_helper'

class MeetingTest < ActiveSupport::TestCase
  test 'month' do
    meeting = Meeting.first
    assert_equal '01', meeting.month
  end
  
  test 'one location filter should work' do
    city = meetings(:one).city
    params = { location: city.name }
    result = Meeting.filter(params)
    
    assert result.location_filters.any?{|v| v.name == city.name}
    assert result.meetings.any?{|m| m.city.name = city.name}
    assert !result.meetings.any?{|m| m.city.name != city.name}
  end

  test 'two location filter should work' do
    locations = [meetings(:one).city.name,meetings(:two).city.name]
    params = { location: locations }
    result = Meeting.filter(params)
    
    assert result.location_filters.any?{|v| (v.name == locations[0]) || (v.name == locations[1])}
    assert !result.meetings.any?{|m| (m.city.name != locations[0] && m.city.name != locations[1])}
  end

  test 'one organizer filter should work' do
    organizer = meetings(:one).organizer
    params = { organizer: organizer }
    result = Meeting.filter(params)

    assert result.meetings.any?{|m| m.organizer = organizer}
    assert !result.meetings.any?{|m| m.organizer != organizer}
  end

  test 'two organizer filter should work' do
    organizers = [meetings(:one).organizer,meetings(:two).organizer]
    params = { organizer: organizers }
    result = Meeting.filter(params)

    assert !result.meetings.any?{|m| (m.organizer != organizers[0] && m.organizer != organizers[1])}
  end

  test 'does allow empty url if meeting is joinable' do
    m = meetings(:one)
    m.url = ''
    m.joinable = true
    assert m.save
  end

  test 'does not allow empty url if meeting is not joinable' do
    m = meetings(:one)
    m.url = ''
    m.joinable = false
    assert !m.save
  end

  test 'join_url should return url if not joinable' do
    m = meetings(:one)
    m.url = 'http://url.dk'
    m.joinable = false
    assert_equal 'http://url.dk', m.join_url
  end

  test 'join_url should return geekhub url if joinable' do
    m = meetings(:one)
    m.url = 'http://url.dk'
    m.joinable = true
    assert_equal "http://www.geekhub.dk/meetings/#{m.id}", m.join_url
  end

  test 'can_attend must return true if capacity is met' do
    m = meetings(:joinable)
    assert m.can_attend?
  end

  test 'can_attend must return false if capacity is met' do
    m = meetings(:joinable)
    a = m.attendees.create
    a.email = 'test@test.dk'
    a.name = 'Jesper'
    a.save
    assert !m.can_attend?
  end

  test "upcomming meetings is available for alerts when no yet alerted and over 3 hours old" do
    Meeting.destroy_all

    available_meeting = Meeting.new({ :created_at => 3.hours.ago - 1.minute, :starts_at => 2.days.from_now })
    available_meeting.save(:validate => false)
    meeting_that_is_too_soon = Meeting.new({ :created_at => 3.hours.ago + 1.minute, :starts_at => 2.days.from_now })
    meeting_that_is_too_soon.save(:validate => false)
    meeting_that_is_already_sent = Meeting.new({ :created_at => 3.hours.ago - 1.minute, :starts_at => 2.days.from_now })
    meeting_that_is_already_sent.meeting_email_alerts.new()
    meeting_that_is_already_sent.save(:validate => false)
    
    meetings = Meeting.available_for_alerts

    assert_equal 1, meetings.length, "should find one meeting"
    assert_equal available_meeting, meetings.first, "should be the available meeting"
  end

  test "upcomming meetings is available for tweets when no yet alerted and over 5 minuttes old" do
    Meeting.destroy_all

    available_meeting = Meeting.new({ :created_at => 6.minutes.ago, :starts_at => 2.days.from_now })
    available_meeting.save(:validate => false)
    meeting_that_is_too_soon = Meeting.new({ :created_at => 4.minutes.ago, :starts_at => 2.days.from_now })
    meeting_that_is_too_soon.save(:validate => false)
    meeting_that_is_already_sent = Meeting.new({ :created_at => 6.minutes.ago, :starts_at => 2.days.from_now })
    meeting_that_is_already_sent.meeting_tweet_alerts.new()
    meeting_that_is_already_sent.save(:validate => false)
    
    meetings = Meeting.available_for_tweet_alerts

    assert_equal 1, meetings.length, "should find one meeting"
    assert_equal available_meeting, meetings.first, "should be the available meeting"
  end
end

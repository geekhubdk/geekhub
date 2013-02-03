require 'test_helper'

class MeetingTest < ActiveSupport::TestCase
  test "month" do
    meeting = Meeting.first
    assert_equal "01", meeting.month
  end
  
  test "one location filter should work" do
    city = meetings(:one).city
    params = { location: city.name }
    result = Meeting.filter(params)
    
    assert_true result.location_filters.any?{|v| v.name == city.name}
    assert_true result.meetings.any?{|m| m.city.name = city.name}
    assert_false result.meetings.any?{|m| m.city.name != city.name}
  end

  test "two location filter should work" do
    locations = [meetings(:one).city.name,meetings(:two).city.name]
    params = { location: locations }
    result = Meeting.filter(params)
    
    assert_true result.location_filters.any?{|v| (v.name == locations[0]) || (v.name == locations[1])}
    assert_false result.meetings.any?{|m| (m.city.name != locations[0] && m.city.name != locations[1])}
  end

  test "one organizer filter should work" do
    organizer = meetings(:one).organizer
    params = { organizer: organizer }
    result = Meeting.filter(params)

    assert_true result.meetings.any?{|m| m.organizer = organizer}
    assert_false result.meetings.any?{|m| m.organizer != organizer}
  end

  test "two organizer filter should work" do
    organizers = [meetings(:one).organizer,meetings(:two).organizer]
    params = { organizer: organizers }
    result = Meeting.filter(params)

    assert_false result.meetings.any?{|m| (m.organizer != organizers[0] && m.organizer != organizers[1])}
  end

  test "does allow empty url if meeting is joinable" do
    m = meetings(:one)
    m.url = ""
    m.joinable = true
    assert_true m.save
  end

  test "does not allow empty url if meeting is not joinable" do
    m = meetings(:one)
    m.url = ""
    m.joinable = false
    assert_false m.save
  end

  test "join_url should return url if not joinable" do
    m = meetings(:one)
    m.url = "http://url.dk"
    m.joinable = false
    assert_equal "http://url.dk", m.join_url
  end

  test "join_url should return geekhub url if joinable" do
    m = meetings(:one)
    m.url = "http://url.dk"
    m.joinable = true
    assert_equal "http://geekhub.dk/meetings/#{m.id}", m.join_url
  end

  test "can_attend must return true if capacity is met" do
    m = meetings(:joinable)
    assert_true m.can_attend?
  end

  test "can_attend must return false if capacity is met" do
    m = meetings(:joinable)
    a = m.attendees.create
    a.email = "test@test.dk"
    a.name = "Jesper"
    a.save
    assert_false m.can_attend?
  end
end

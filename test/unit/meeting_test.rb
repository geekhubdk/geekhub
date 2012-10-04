require 'test_helper'

class MeetingTest < ActiveSupport::TestCase
  test "month" do
    meeting = Meeting.first
    assert_equal "12", meeting.month
  end
  
  test "one location filter should work" do
    location = meetings(:one).location
    params = { location: location }
    result = Meeting.filter(params)
    
    assert_true result.location_filters.any?{|v| v == location}
    assert_true result.meetings.any?{|m| m.location = location}
    assert_false result.meetings.any?{|m| m.location != location}
  end

  test "two location filter should work" do
    locations = [meetings(:one).location,meetings(:two).location]
    params = { location: locations }
    result = Meeting.filter(params)
    
    assert_true result.location_filters.any?{|v| (v == locations[0]) || (v == locations[1])}
    assert_false result.meetings.any?{|m| (m.location != locations[0] && m.location != locations[1])}
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
end

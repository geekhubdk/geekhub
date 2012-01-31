require 'test_helper'

class MeetingTest < ActiveSupport::TestCase
  test "month" do
    meeting = Meeting.first
    assert_equal "01", meeting.month
  end

  test "to_ical" do
    meeting = Meeting.first
    assert_not_nil meeting.to_ical
  end
end

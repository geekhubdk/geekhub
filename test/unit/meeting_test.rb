require 'test_helper'

class MeetingTest < ActiveSupport::TestCase
  test "month" do
    meeting = Meeting.first
    assert_equal "12", meeting.month
  end
end

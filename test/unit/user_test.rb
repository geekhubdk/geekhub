require 'test_helper'

class UserTest < ActiveSupport::TestCase
  test "vote_on must only vote one time on event" do
    user = users(:one)
    meeting = meetings(:one)

    user.vote_on(meeting)
    user.vote_on(meeting)

    assert_equal 1, user.meeting_votes.count
  end

  test "vote_on must vote two times, if different events" do
    user = users(:one)
    meeting = meetings(:one)
    meeting2 = meetings(:two)

    user.vote_on(meeting)
    user.vote_on(meeting2)

    assert_equal 2, user.meeting_votes.count
  end

  
end

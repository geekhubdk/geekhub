require 'test_helper'

SimpleCov.command_name 'Unit Tests' 

class MeetingRevisionTest < ActiveSupport::TestCase
  test "month" do
    revision = MeetingRevision.first
    assert_equal "01", revision.month
  end

  test "approve" do
    revision = MeetingRevision.first
    revision.title = "new title"
    revision.approve
    assert_not_nil revision.approved_at
    assert_equal revision.title, revision.meeting.title
  end
end

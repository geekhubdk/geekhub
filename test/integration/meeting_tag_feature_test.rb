require 'test_helper'

class MeetingTagFeatureTest < ActionDispatch::IntegrationTest

  test "creating a meeting with a list of tags" do
    m = meetings(:future)
    m.meeting_tags.create(tag: Tag.find_or_create_by(name: ".NET"))
    m.meeting_tags.create(tag: Tag.find_or_create_by(name: "JavaScript"))
    assert m.save
  end

  test "getting a list of meetings with a any of the supplied tags" do
    meetings = Meeting.filter({all: "1", tag:[".NET", "JavaScript"]}).meetings
    assert_equal 2, meetings.length
  end

end
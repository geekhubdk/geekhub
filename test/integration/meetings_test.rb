require 'test_helper'

class MeetingsTest < ActionDispatch::IntegrationTest
  test "should be able to go to meeting" do
    visit root_path
    click_on meetings(:one).title

    assert_equal meeting_path(meetings(:one)), current_path
  end

  test "the detail page should have the meetings information" do
    meeting = meetings(:one)
    visit meeting_path(meeting)

    assert has_selector?('h1', text: meeting.title)
    assert has_link?("readmore", href: meeting.url)
    assert has_content?(meeting.organizer)
    assert has_content?(meeting.location)
    assert has_content?(meeting.description)
  end
end

require 'test_helper'

class MeetingsTest < ActionDispatch::IntegrationTest
  test "should be able to go to meeting" do
    visit root_path
    click_on meetings(:one).title

    assert_equal meeting_path(meetings(:one)), current_path
  end
end

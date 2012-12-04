require 'test_helper'

class MeetingSuggestionsControllerTest < ActionController::TestCase
  test "should create meeting sugestion" do
    assert_difference('MeetingSuggestion.count') do
      post :create, url: "http://google.dk"
    end

    assert_redirected_to root_path()
  end
end

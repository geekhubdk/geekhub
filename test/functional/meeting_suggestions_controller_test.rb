require 'test_helper'

class MeetingSuggestionsControllerTest < ActionController::TestCase
  test "should get index, as rss" do
    get :index, format: :rss
    assert_response :success
  end

  test "should create meeting sugestion" do
    assert_difference('MeetingSuggestion.count') do
      post :create, meeting_suggestion: { url: "http://google.dk" }
    end

    assert_redirected_to root_path()
  end

  test "should not create meeting sugestion, if invalid" do
    assert_difference('MeetingSuggestion.count', 0) do
      post :create
    end

    assert_redirected_to root_path()
  end
end

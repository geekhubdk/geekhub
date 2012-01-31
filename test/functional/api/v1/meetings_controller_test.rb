require 'test_helper'

class Api::V1::MeetingsControllerTest < ActionController::TestCase
  setup do
    @meeting = meetings(:one)
  end

  test "should get index" do
    get :index, :format => :json 
    assert_response :success
  end
end
require 'test_helper'

class MeetingsControllerTest < ActionController::TestCase
  include Devise::TestHelpers

  setup do
    @meeting = meetings(:one)
  end

  test "should get index" do
    get :index

    assert_redirected_to root_path
  end

  test "should get index as rss" do
    get :index, :format => :rss 
    assert_response :success
  end
  
  test "should get new" do
    sign_in User.first
    get :new
    assert_response :success
  end

  test "should create meeting" do
    sign_in User.first
    assert_difference('Meeting.count') do
      post :create, meeting: @meeting.attributes
    end

    assert_redirected_to root_path()
  end
  
  test "should not create meeting, if invalid" do
    sign_in User.first
    post :create, meeting: nil

    assert_response :success
  end

  test "should get edit" do
    sign_in User.first
    get :edit, id: @meeting.to_param
    assert_response :success
  end
  
  test "should redirect to login page, when visiting edit page, if not logged in" do
    sign_in User.find(users(:two).id)
    get :edit, id: @meeting.to_param
    assert_redirected_to new_user_session_path
  end
  
  test "should redirect to login page, when visiting edit page, if you cant edit meeting" do
    get :edit, id: @meeting.to_param
    assert_redirected_to new_user_session_path
  end
  
  test "should update meeting" do
    sign_in User.first
    put :update, id: @meeting.to_param, meeting: @meeting.attributes, approve: true
    assert_redirected_to root_path()
  end

  test "should not update meeting, if invalid" do
    sign_in User.first
    put :update, id: @meeting.to_param, meeting: {title: nil}
    assert_response :success
  end

  test "should destroy meeting" do
    sign_in User.first
    assert_difference('Meeting.count', -1) do
      delete :destroy, id: @meeting.to_param
    end

    assert_redirected_to root_path
  end

  test "should display show without logging in" do
    get :show, id: @meeting.to_param
    assert_equal false, assigns[:can_vote], "People need to login to vote"
  end

  test "should display show with login" do
    sign_in User.first

    get :show, id: @meeting.to_param
    assert_equal true, assigns[:can_vote], "If a person is logged in, and has not voted before, they should be able to vote"
  end
    
  test "should display show without can_vote if voted before" do
    User.first.meeting_votes.create({ meeting_id: @meeting.id })

    get :show, id: @meeting.to_param
    assert_equal false, assigns[:can_vote], "Should not be able to vote, if have voted before"
  end

  test "vote on should redirect back to meeting after vote" do
    sign_in User.first

    post :vote, id: @meeting.to_param

    assert_redirected_to @meeting
  end
end

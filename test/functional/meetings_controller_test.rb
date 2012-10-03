require 'test_helper'

class MeetingsControllerTest < ActionController::TestCase
  include Devise::TestHelpers

  setup do
    @meeting = meetings(:one)
  end

  test "should get index" do
    get :index
    assert_response :success
    assert_not_nil assigns(:meetings)
  end

  test "should contain filter attribute" do
  	get :index
  	assert_not_nil assigns(:location_filters)
  	assert_true assigns(:location_filters).any?{|v| v == meetings(:one).location}
  end

  test "one location filter should work" do
    location = meetings(:one).location
    get :index, location: location

    assert_true assigns(:location_filters).any?{|v| v == location}
    assert_true assigns(:meetings).any?{|m| m.location = location}
    assert_false assigns(:meetings).any?{|m| m.location != location}
  end

  test "two location filter should work" do
    locations = [meetings(:one).location,meetings(:two).location]
    get :index, location: locations

    assert_true assigns(:location_filters).any?{|v| (v == locations[0]) || (v == locations[1])}
    assert_false assigns(:meetings).any?{|m| (m.location != locations[0] && m.location != locations[1])}
  end

  test "one organizer filter should work" do
    organizer = meetings(:one).organizer
    get :index, organizer: organizer

    assert_true assigns(:meetings).any?{|m| m.organizer = organizer}
    assert_false assigns(:meetings).any?{|m| m.organizer != organizer}
  end

  test "two organizer filter should work" do
    organizers = [meetings(:one).organizer,meetings(:two).organizer]
    get :index, organizer: organizers

    assert_false assigns(:meetings).any?{|m| (m.organizer != organizers[0] && m.organizer != organizers[1])}
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

end

require 'test_helper'

SimpleCov.command_name 'Functional Tests' 

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

  test "should get index, with non-approved" do
    get :index, :approved => "0" 
    assert_response :success
    assert_not_nil assigns(:meetings)
    assert_equal :approve, assigns(:mode)
  end

  test "should get index as rss" do
    get :index, :format => :rss 
    assert_response :success
  end

  test "should get new" do
    get :new
    assert_response :success
  end

  test "should get show, as ics" do
    get :show, id: @meeting.to_param, :format => :ics 
    assert_response :success
  end

  test "should create meeting" do
    assert_difference('Meeting.count') do
      post :create, meeting: @meeting.attributes
    end

    assert_redirected_to meetings_path()
  end
  
  test "should get edit" do
    sign_in User.first
    get :edit, id: @meeting.to_param
    assert_response :success
  end

  test "should get suggest edit" do
    get :edit, id: @meeting.to_param
    assert_response :success

    assert assigns[:meeting].suggested_by.nil?
  end

  test "should update meeting" do
    sign_in User.first
    put :update, id: @meeting.to_param, meeting: @meeting.attributes
    assert_redirected_to meetings_path()
  end

  test "should create meeting revision if not signed in" do
    assert_difference 'MeetingRevision.count' do
      attributes = @meeting.attributes
      attributes.delete('approved_at')
      put :update, id: @meeting.to_param, meeting: attributes
    end
  end

  test "should destroy meeting" do
    sign_in User.first
    assert_difference('Meeting.count', -1) do
      delete :destroy, id: @meeting.to_param
    end

    assert_redirected_to meetings_path
  end
end

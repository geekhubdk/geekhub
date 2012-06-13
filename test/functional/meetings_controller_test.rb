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
    assert_equal 0, assigns(:meetings).take_while{|m| m.approved_at == nil }.length
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

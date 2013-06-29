require 'test_helper'

class MeetingsControllerTest < ActionController::TestCase
  include Devise::TestHelpers

  setup do
    @meeting = meetings(:one)
  end

  test 'should get index' do
    get :index
    assert_response :success
    assert_not_nil assigns(:meetings)
  end

  test 'should contain filter attribute' do
    get :index
  	assert_not_nil assigns(:location_filters)
  	assert_true assigns(:location_filters).any?{|v| v.name == meetings(:one).city.name}
  end

  test 'should get index as rss' do
    get :index, :format => :rss 
    assert_response :success
  end
  
  test 'should get new' do
    sign_in User.first
    get :new
    assert_response :success
  end

  test 'should create meeting' do
    sign_in User.first
    assert_difference('Meeting.count') do
      post :create, meeting: @meeting.attributes
    end

    assert_equal I18n.t('meeting.added'), flash[:notice]
    assert_redirected_to root_path()
  end
  
  test 'should not create meeting, if invalid' do
    sign_in User.first
    post :create, meeting: nil

    assert_response :success
  end

  test 'should get edit' do
    sign_in User.first
    get :edit, id: @meeting.to_param
    assert_response :success
  end
  
  test 'should redirect to login page, when visiting edit page, if not logged in' do
    sign_in User.find(users(:two).id)
    get :edit, id: @meeting.to_param
    assert_redirected_to new_user_session_path
  end
  
  test 'should redirect to login page, when visiting edit page, if you cant edit meeting' do
    get :edit, id: @meeting.to_param
    assert_redirected_to new_user_session_path
  end
  
  test 'should update meeting' do
    sign_in User.first
    put :update, id: @meeting.to_param, meeting: @meeting.attributes, approve: true
    
    assert_equal I18n.t('meeting.updated'), flash[:notice]
    assert_redirected_to root_path()
  end

  test 'should not update meeting, if invalid' do
    sign_in User.first
    put :update, id: @meeting.to_param, meeting: {title: nil}
    assert_response :success
  end

  test 'should destroy meeting' do
    sign_in User.first
    assert_difference('Meeting.count', -1) do
      delete :destroy, id: @meeting.to_param
    end

    assert_redirected_to root_path
  end

  test 'should get typeahead_address' do
    # not a good test
    sign_in User.first
    get :typeahead_address, query: 'test', format: :json
    assert_response :ok
  end

  test 'should get typeahead_address, with city' do
    # not a good test
    sign_in User.first
    get :typeahead_address, query: 'test', city: cities(:odense).id, format: :json
    assert_response :ok
  end

  test 'should get typeahead_organizers' do
    # not a good test
    sign_in User.first
    get :typeahead_organizers, query: 'test', format: :json
    assert_response :ok
  end
  
  test 'should show meeting' do
    get :show, :id => meetings(:one).id
  end
  
  test 'archive should get all meetings in reverse order' do
    
    get :archive
    
    @meetings = assigns(:meetings)
    assert_not_nil(@meetings)
    assert_equal(meetings(:future).id, @meetings.first.id)
  end
end

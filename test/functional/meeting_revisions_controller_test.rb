require 'test_helper'

class MeetingRevisionsControllerTest < ActionController::TestCase
  include Devise::TestHelpers
  
  setup do
    @meeting_revision = meeting_revisions(:one)
  end

  test "should get edit" do
    sign_in User.first
    get :edit, id: @meeting_revision.to_param
    assert_response :success
  end

  test "should update meeting" do
    sign_in User.first
    post :approve, id: @meeting_revision.to_param, meeting_revision: @meeting_revision.attributes
    assert_redirected_to meetings_path()
  end
end
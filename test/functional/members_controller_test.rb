require 'test_helper'

class MembersControllerTest < ActionController::TestCase
  # test "the truth" do
  #   assert true
  # end
  
  test ".authenticate" do
	  member = Member.first
	  authenticator_mock = mock()
	  authenticator_mock.expects(:login).returns(member)
	  
	  MembersController.any_instance.expects(:authenticator).returns(authenticator_mock)
	  
	  get :authenticate, email: "test@exsample.org", password: "pass"
	  
	  assert_redirected_to member_path(member)
	end
end

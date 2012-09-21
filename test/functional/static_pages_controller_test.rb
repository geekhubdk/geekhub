require 'test_helper'

class StaticPagesControllerTest < ActionController::TestCase
  test "should get datapolitik" do
    get :datapolitik
    assert_response :success
  end

end

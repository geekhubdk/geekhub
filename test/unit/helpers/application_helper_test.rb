require 'test_helper'
require 'action_view/test_case'
require 'action_view/helpers'

class ApplicationHelperTest < ActionView::TestCase
  include ERB::Util

  test "month_name" do
    assert_equal "februar", month_name("02")
  end
end

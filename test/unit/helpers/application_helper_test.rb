require 'test_helper'
require 'action_view/test_case'
require 'action_view/helpers'

class ApplicationHelperTest < ActionView::TestCase
  include ERB::Util

  test "format_rss_description" do
    result = format_rss_description(Meeting.first)
    assert_not_nil result  
  end

  test "month_name" do
    assert_equal "februar", month_name("02")
  end
end

require 'test_helper'

class ApplicationControllerTest < ActionController::TestCase
  test "boolean_param should return nil if nil" do
    assert_equal nil, @controller.send(:boolean_param, "test")
  end
  
  test "boolean_param should default_value if nil" do
    assert_equal false, @controller.send(:boolean_param, "test", false)
  end
  
  test "boolean_param should return value if true value" do
    @controller.params[:test] = true
    assert_equal true, @controller.send(:boolean_param, "test")
    @controller.params[:test] = "true"
    assert_equal true, @controller.send(:boolean_param, "test")
    @controller.params[:test] = "1"
    assert_equal true, @controller.send(:boolean_param, "test")
  end
  
  test "boolean_param should return nil if value is of the wrong type" do
    @controller.params[:test] = "this_is_not_a_boolean_value"
    assert_equal nil, @controller.send(:boolean_param, "test")
  end
  
  test "boolean_param should return default value if value is of the wrong type" do
    @controller.params[:test] = "this_is_not_a_boolean_value"
    assert_equal false, @controller.send(:boolean_param, "test", false)
  end
  
  test "boolean_param should return false if false value" do
    @controller.params[:test] = false
    assert_equal false, @controller.send(:boolean_param, "test")
    @controller.params[:test] = "false"
    assert_equal false, @controller.send(:boolean_param, "test")
    @controller.params[:test] = "0"
    assert_equal false, @controller.send(:boolean_param, "test")
  end
  
  test "integer_param should return nil if nil" do
    assert_equal nil, @controller.send(:integer_param, "test")
  end
  
  test "integer_param should default_value if nil" do
    assert_equal 0, @controller.send(:integer_param, "test", 0)
  end
  
  test "integer_param should return value if number" do
    @controller.params[:test] = "10"
    assert_equal 10, @controller.send(:integer_param, "test")
  end

  test "integer_param should return nil if number is invalid" do
    @controller.params[:test] = "NAN"
    assert_equal nil, @controller.send(:integer_param, "test")
  end

  test "integer_param should return default_value if number is invalid" do
    @controller.params[:test] = "NAN"
    assert_equal 10, @controller.send(:integer_param, "test",10)
  end

  
end
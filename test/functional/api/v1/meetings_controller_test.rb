require 'test_helper'

class Api::V1::MeetingsControllerTest < ActionController::TestCase
  setup do
    @meeting = meetings(:one)
  end

  test "should get index" do
    get :index, :format => :json 
    assert_response :success
    assert_not_nil assigns[:meetings]

    assert_false assigns[:meetings].any?{|m| m.starts_at<Time.now}
  end

  test "should get index with all" do
    get :index, :all => "1", :format => :json
    assert_response :success
    assert_not_nil assigns[:meetings]
    assert_true assigns[:meetings].any?{|m| m.starts_at<Time.now}
  end

  test "should get index with organizer filter" do
    get :index, :organizer => "onug", :all => "1", :format => :json
    assert_response :success
    assert_not_nil assigns[:meetings]
    assert_true assigns[:meetings].any?{|m| m.organizer == "ONUG"}
    assert_false assigns[:meetings].any?{|m| m.organizer != "ONUG"}
  end
  
  test "should get index with organizer filter array" do
    get :index, :organizer => ["ONUG", "ANUG"], :all => "1", :format => :json
    assert_response :success
    assert_not_nil assigns[:meetings]
    assert_true assigns[:meetings].any?{|m| m.organizer == "ONUG"}
    assert_true assigns[:meetings].any?{|m| m.organizer == "ANUG"}
    assert_false assigns[:meetings].any?{|m| m.organizer != "ONUG" && m.organizer != "ANUG"}
  end

  test "should get index with location filter" do
    get :index, :location => "aarhus", :all => "1", :format => :json
    assert_response :success
    assert_not_nil assigns[:meetings]
    assert_true assigns[:meetings].any?{|m| m.location == "Aarhus"}
    assert_false assigns[:meetings].any?{|m| m.location != "Aarhus"}
  end
  
  test "should get index with location filter array" do
    get :index, :location => ["aarhus", "odense"], :all => "1", :format => :json
    assert_response :success
    assert_not_nil assigns[:meetings]
    assert_true assigns[:meetings].any?{|m| m.location == "Aarhus"}
    assert_true assigns[:meetings].any?{|m| m.location == "Odense"}
    assert_false assigns[:meetings].any?{|m| m.location != "Odense" && m.location != "Aarhus"}
  end

  test "should get index with timeline" do
    get :index, :format => :timeline 
    assert_response :success
    assert_not_nil assigns[:meetings]
  end

end
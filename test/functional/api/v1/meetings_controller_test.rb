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
    assert_true assigns[:meetings].any?{|m| m.city.name == "Aarhus"}
    assert_false assigns[:meetings].any?{|m| m.city.name != "Aarhus"}
  end
  
  test "should get index with location filter array" do
    get :index, :location => ["aarhus", "odense"], :all => "1", :format => :json
    assert_response :success
    assert_not_nil assigns[:meetings]
    assert_true assigns[:meetings].any?{|m| m.city.name == "Aarhus"}
    assert_true assigns[:meetings].any?{|m| m.city.name == "Odense"}
    assert_false assigns[:meetings].any?{|m| m.city.name != "Odense" && m.city.name != "Aarhus"}
  end

  test "should be able to create meeting" do
    post :create, meeting: { title: "My title", starts_at: Time.now, description: "Something", url: "http://something.dk", organizer: "ONUG", location: cities(:odense).name, costs_money: false }

    assert_response 302
  end
end
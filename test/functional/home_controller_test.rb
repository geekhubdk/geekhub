require 'test_helper'

class HomeControllerTest < ActionController::TestCase
  test "should get index" do
    get :index
    assert_response :success
    assert_not_nil assigns(:meetings)
  end

  test "should contain filter attribute" do
  	get :index
  	assert_not_nil assigns(:location_filters)
  	assert_true assigns(:location_filters).any?{|v| v == meetings(:one).location}
  end

  test "one location filter should work" do
    location = meetings(:one).location
    get :index, location: location

    assert_true assigns(:location_filters).any?{|v| v == location}
    assert_true assigns(:meetings).any?{|m| m.location = location}
    assert_false assigns(:meetings).any?{|m| m.location != location}
  end

  test "two location filter should work" do
    locations = [meetings(:one).location,meetings(:two).location]
    get :index, location: locations

    assert_true assigns(:location_filters).any?{|v| (v == locations[0]) || (v == locations[1])}
    assert_false assigns(:meetings).any?{|m| (m.location != locations[0] && m.location != locations[1])}
  end

  test "one organizer filter should work" do
    organizer = meetings(:one).organizer
    get :index, organizer: organizer

    assert_true assigns(:meetings).any?{|m| m.organizer = organizer}
    assert_false assigns(:meetings).any?{|m| m.organizer != organizer}
  end

  test "two organizer filter should work" do
    organizers = [meetings(:one).organizer,meetings(:two).organizer]
    get :index, organizer: organizers

    assert_false assigns(:meetings).any?{|m| (m.organizer != organizers[0] && m.organizer != organizers[1])}
  end

end

require 'test_helper'

class MeetingEmailAlertSubscriptionsControllerTest < ActionController::TestCase
  include Devise::TestHelpers
  
  test "shows subscribe if the user is not subscribed" do
    User.first.meeting_email_alert_subscriptions.destroy_all
    
    sign_in User.first
    
    get :index
    
    assert_not_nil(assigns[:subscription])
    assert(assigns[:show_subscribe], "Should have true in show subscribe")
    assert_false(assigns[:show_unsubscribe], "Should have false in show unsubscribe")
  end
  
  test "Create should make a subscription on the user" do
    User.first.meeting_email_alert_subscriptions.destroy_all
    sign_in User.first
    
    assert_difference "MeetingEmailAlertSubscription.count", +1 do
      post :create
    end
    
    assert_equal 1, User.first.meeting_email_alert_subscriptions.count
  end
  
  test "Destroy should unsubscribe the user" do
    User.first.meeting_email_alert_subscriptions.destroy_all
    
    sign_in User.first
    
    subscription = User.first.meeting_email_alert_subscriptions.create
    
    assert_difference "MeetingEmailAlertSubscription.count", -1 do
      delete :destroy, id: subscription.id
    end
    
    assert_equal 0, User.first.meeting_email_alert_subscriptions.count
  end
end

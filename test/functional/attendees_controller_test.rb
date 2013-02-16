require 'test_helper'

class AttendeesControllerTest < ActionController::TestCase
  test "Create a attendee" do
    ActionMailer::Base.deliveries.clear
    assert_difference "Attendee.count" do
      post :create, meeting_id: meetings(:one).id, attendee: { email: "test@test.dk", name: "test" }
    end
    assert_equal 1, ActionMailer::Base.deliveries.count
    assert_redirected_to meeting_path(meetings(:one).id)
  end

  test "Does not create attendee, if email is already attending" do
    ActionMailer::Base.deliveries.clear
    meeting = Meeting.find(meetings(:one).id)
    meeting.attendees.create({ email: "test@test.dk", name: "test2" })

    assert_difference "Attendee.count",0 do
      post :create, meeting_id: meetings(:one).id, attendee: { email: "test@test.dk", name: "test" }
    end
    assert_equal 0, ActionMailer::Base.deliveries.count
    assert_redirected_to meeting_path(meetings(:one).id)
  end

  test "Create attendee, if email is already attending another meeting" do
    ActionMailer::Base.deliveries.clear
    meeting = Meeting.find(meetings(:two).id)
    meeting.attendees.create({ email: "test@test.dk", name: "test2" })

    assert_difference "Attendee.count",1 do
      post :create, meeting_id: meetings(:one).id, attendee: { email: "test@test.dk", name: "test" }
    end
    assert_equal 1, ActionMailer::Base.deliveries.count
    assert_redirected_to meeting_path(meetings(:one).id)
  end
end

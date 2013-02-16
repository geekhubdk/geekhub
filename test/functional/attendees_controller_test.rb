require 'test_helper'

class AttendeesControllerTest < ActionController::TestCase
  test "Create a attendee" do
    ActionMailer::Base.deliveries.clear
    assert_difference "Attendee.count" do
      post :create, meeting_id: meetings(:one).id, attendee: { email: "test@test.dk", name: "test" }
    end
    assert_equal I18n.t("attendee.attending"), flash[:notice]
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
    assert_equal I18n.t("attendee.already_attending"), flash[:error]
    assert_redirected_to meeting_path(meetings(:one).id)
  end

  test "Create attendee, if email is already attending another meeting" do
    ActionMailer::Base.deliveries.clear
    meeting = Meeting.find(meetings(:two).id)
    meeting.attendees.create({ email: "test@test.dk", name: "test2" })

    assert_difference "Attendee.count",1 do
      post :create, meeting_id: meetings(:one).id, attendee: { email: "test@test.dk", name: "test" }
    end

    assert_equal I18n.t("attendee.attending"), flash[:notice]
    assert_equal 1, ActionMailer::Base.deliveries.count
    assert_redirected_to meeting_path(meetings(:one).id)
  end

  test "Destroy attendee, by user" do
    ActionMailer::Base.deliveries.clear
    sign_in User.first

    meeting = Meeting.find(meetings(:one).id)
    attendee = meeting.attendees.create({ email: "test@test.dk", name: "test", user_id: User.first.id })

    assert_difference "Attendee.count", -1 do
      delete :destroy, meeting_id: meeting.id, id: attendee.id
    end

    assert_equal I18n.t("attendee.destroyed"), flash[:notice]
    assert_equal 1, ActionMailer::Base.deliveries.count
    assert_redirected_to meeting_path(meeting)
  end

  test "Destroy attendee, by email" do
    ActionMailer::Base.deliveries.clear
    sign_in User.first
    
    meeting = Meeting.find(meetings(:one).id)
    attendee = meeting.attendees.create({ email: "test@test.dk", name: "test"})

    assert_difference "Attendee.count", -1 do
      delete :destroy_attendee, meeting_id: meeting.id, email: attendee.email.upcase
    end

    assert_equal I18n.t("attendee.destroyed"), flash[:notice]
    assert_equal 1, ActionMailer::Base.deliveries.count
    assert_redirected_to meeting_path(meeting)
  end

  test "Does not destroy a attendee, if email is not found" do
    ActionMailer::Base.deliveries.clear
    
    meeting = Meeting.find(meetings(:one).id)
    attendee = meeting.attendees.create({ email: "test@test.dk", name: "test"})

    assert_difference "Attendee.count", 0 do
      delete :destroy_attendee, meeting_id: meeting.id, email: attendee.email + "wrong"
    end

    assert_equal I18n.t("attendee.not_found_by_email"), flash[:error]
    assert_equal 0, ActionMailer::Base.deliveries.count
    assert_redirected_to meeting_path(meeting)
  end
end

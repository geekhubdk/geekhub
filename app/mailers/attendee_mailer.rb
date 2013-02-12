class AttendeeMailer < ActionMailer::Base
  default from: "hello@geekhub.dk"

  def new_attendee_email attendee
    @attendee = attendee
    mail(:to => attendee.meeting.user.email, :subject => "Ny deltager til '#{attendee.meeting.title}'")
  end

  def destroy_attendee_email attendee
    @attendee = attendee
    mail(:to => attendee.meeting.user.email, :subject => "Deltager afmeldt til '#{attendee.meeting.title}'")
  end
end

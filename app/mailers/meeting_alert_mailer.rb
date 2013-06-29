# encoding: UTF-8
class MeetingAlertMailer < ActionMailer::Base
  helper :application
  default from: 'Geekhub.dk <hello@geekhub.dk>'

  # Subject can be set in your I18n file at config/locales/en.yml
  # with the following lookup:
  #
  #   en.meeting_alert_mailer.alert.subject
  #
  def alert_email meetings, email

    subject = "#{meetings.count} events er blivet oprettet på Geekhub."

    if meetings.length == 1
      subject = "Nyt event på Geekhub: #{meetings.map{|m| m.title}.join(' :: ')}"
    elsif meetings.length < 3
      subject = "#{meetings.count} nye events på Geekhub: #{meetings.map{|m| m.title}.join(' :: ')}"
    end

    @meetings = meetings

    mail to: email, subject: subject
  end
end

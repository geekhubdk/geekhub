class MeetingEmailAlertSubscriptionsController < ApplicationController
  def index
    if current_user
      @subscription = current_user.meeting_email_alert_subscriptions.first || current_user.meeting_email_alert_subscriptions.new

      @show_subscribe = @subscription.new_record?
      @show_unsubscribe = !@subscription.new_record?
    end
  end

  def create
    current_user.meeting_email_alert_subscriptions.create
    redirect_to meeting_email_alert_subscriptions_path, notice: 'Du er hermed tilmeldt'
  end

  def destroy
    current_user.meeting_email_alert_subscriptions.find(params[:id]).destroy
    redirect_to meeting_email_alert_subscriptions_path, notice: 'Du er hermed afmeldt'
  end
end

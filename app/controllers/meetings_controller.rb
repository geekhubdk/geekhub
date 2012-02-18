class MeetingsController < ApplicationController

  before_filter :authenticate_user!, :only => [:destroy, :edit, :update]

  helper_method :can_approve_meeting?

  def index
    @meetings = Meeting.upcomming.order("starts_at")

    if params[:approved] == "0"
      # approve mode, is visible to all, if they really like
      @mode = :approve
      @meetings = @meetings.needs_approval
    else
      @mode = :upcomming
      @meetings = @meetings.approved
    end

    respond_to do |format|
      format.html
      format.rss do
        # Only show meetings in the 14 days
        @meetings = @meetings.where("starts_at < ?", Time.now + 14.days)
      end
    end
  end

  def show
    @meeting = Meeting.find(params[:id])

    respond_to do |format|
      format.ics { send_data(@meeting.to_ical.export, :filename=>"event.ics", :disposition=>"inline; filename=event.ics", :type=>"text/calendar")}
      format.html
    end
  end

  def new
    @meeting = Meeting.new
    @meeting.starts_at = 1.month.from_now.to_date

    @mode = can_approve_meeting? ? :create : :approve
  end

  def edit
    @meeting = Meeting.find(params[:id])
  end

  def create
    @meeting = Meeting.new(params[:meeting])

    @meeting.approved_at = can_approve_meeting? ? Time.now : nil

    if @meeting.save
      redirect_to meetings_path, notice: 'Meeting was successfully created.'
    else
      render action: "new"
    end
  end

  def update
    @meeting = Meeting.find(params[:id])

    unless params[:approve].nil?
      @meeting.approved_at = Time.now
    end

    if @meeting.update_attributes(params[:meeting])
      redirect_to meetings_path, notice: 'Meeting was successfully updated.'
    else
      render "edit"
    end
  end

  def destroy
    @meeting = Meeting.find(params[:id])
    @meeting.destroy

    redirect_to meetings_url
  end

private

  def can_approve_meeting?
    user_signed_in?
  end

end

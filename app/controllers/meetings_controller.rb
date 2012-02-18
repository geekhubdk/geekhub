class MeetingsController < ApplicationController
  before_filter :authenticate_user!, :only => [:destroy, :edit, :update]
  helper_method :can_approve_meeting?

  def index
    filters = {
      upcomming: boolean_param(:upcomming, true),
      approved: boolean_param(:approved, true),
      days_from_now: integer_param(:days_from_now, 0)
    }
  
    @meetings = Meeting.order("starts_at")
    @meetings = @meetings.upcomming if filters[:upcomming] == true
    @meetings = filters[:approved] ? @meetings.approved : @meetings.needs_approval
    @meetings = @meetings.where("starts_at < ?", Time.now + filters[:days_from_now].to_i.days) if filters[:days_from_now].to_i > 0

    respond_to do |format|
      format.html
      format.rss
    end
  end

  def show
    @meeting = Meeting.find(params[:id])
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

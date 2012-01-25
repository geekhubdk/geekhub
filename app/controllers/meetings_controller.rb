class MeetingsController < ApplicationController

  before_filter :authenticate_user!, :only => [:destroy]

  helper_method :can_approve_meeting?

  def index
    @meetings = Meeting.upcomming.order("starts_at")

    if params[:approved] == "0"
      # approve mode, is visible to all, if they really like
      @mode = :approve
      @meetings = @meetings.needs_approval + MeetingRevision.needs_approval.order("starts_at") 
    else
      @mode = :upcomming
      @meetings = @meetings.approved
    end

    respond_to do |format|
      format.html
      format.json { render json: @meetings }
      format.rss do
        # Only show meetings in the next month
        @meetings = @meetings.where("starts_at < ?", Time.now + 1.month)
      end
    end
  end

  def show
    @meeting = Meeting.find(params[:id])

    respond_to do |format|
      format.ics { send_data(@meeting.to_ical.export, :filename=>"event.ics", :disposition=>"inline; filename=event.ics", :type=>"text/calendar")}
    end
  end

  def new
    @meeting = Meeting.new
    @meeting.starts_at = 1.month.from_now.to_date

    @mode = can_approve_meeting? ? :create : :approve

    respond_to do |format|
      format.html # new.html.erb
      format.json { render json: @meeting }
    end
  end

  def edit
    @meeting = Meeting.find(params[:id])

    unless can_approve_meeting?
      @meeting.suggested_by = nil
      render 'suggest_edit'
    end
  end

  def suggest_edit
    @meeting = Meeting.find(params[:id])
  end

  def create
    @meeting = Meeting.new(params[:meeting])

    @meeting.approved_at = can_approve_meeting? ? Time.now : nil

    respond_to do |format|
      if @meeting.save
        format.html { redirect_to meetings_path, notice: 'Meeting was successfully created.' }
        format.json { render json: @meeting, status: :created, location: @meeting }
      else
        format.html { render action: "new" }
        format.json { render json: @meeting.errors, status: :unprocessable_entity }
      end
    end
  end

  def update
    @meeting = Meeting.find(params[:id])

    unless params[:approve].nil?
      @meeting.approved_at = Time.now
    end

    # if they want to make a suggestion, then return that
    unless can_approve_meeting?
      return create_meeting_suggestion
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

    respond_to do |format|
      format.html { redirect_to meetings_url }
      format.json { head :ok }
    end
  end

  private

  def create_meeting_suggestion
    if @meeting.meeting_revisions.create(params[:meeting])
      redirect_to meetings_path, notice: 'Dit forslag er indsendt.'
    else
      render "suggest_edit"
    end
  end

  def can_approve_meeting?
    user_signed_in?
  end

end

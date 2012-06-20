class MeetingsController < ApplicationController
  before_filter :authenticate_user!, :except => [:index, :show]

  def index
    filters = {
      upcomming: boolean_param(:upcomming, true),
      approved: boolean_param(:approved, true),
      days_from_now: integer_param(:days_from_now, 0)
    }
  
    @meetings = Meeting.filter(filters)

    respond_to do |format|
      format.html
      format.rss
      format.json do
        render :json => @meetings
      end
    end
  end

  def show
    @meeting = Meeting.find(params[:id])
    
    if user_signed_in?
      @can_vote = @meeting.meeting_votes.where("user_id = ?", current_user.id).empty?
    else
      @can_vote = false
    end
 
  end

  def vote
    meeting = Meeting.find(params[:id])
    
    current_user.vote_on(meeting)

    redirect_to meeting
  end

  def new
    @meeting = Meeting.new
    @meeting.starts_at = 7.days.from_now.to_date + 16.hours
  end

  def edit
    @meeting = Meeting.find(params[:id])

    unless @meeting.can_be_edited_by current_user
      redirect_to new_user_session_path
    end
  end

  def create
    @meeting = Meeting.new(params[:meeting])

    @meeting.user = current_user

    if @meeting.save
      redirect_to root_path, notice: 'Meeting was successfully created.'
    else
      render action: "new"
    end
  end

  def update
    @meeting = Meeting.find(params[:id])

    unless @meeting.can_be_edited_by current_user
      redirect_to new_user_session_path
    end

    if @meeting.user.nil?
      @meeting.user = current_user
    end

    if @meeting.update_attributes(params[:meeting])
      redirect_to root_path, notice: 'Meeting was successfully updated.'
    else
      render "edit"
    end
  end

  def destroy
    @meeting = Meeting.find(params[:id])

    unless @meeting.can_be_edited_by current_user
      redirect_to new_user_session_path
    end

    @meeting.destroy

    redirect_to root_path
  end

private

  def can_approve_meeting?
    user_signed_in?
  end

end

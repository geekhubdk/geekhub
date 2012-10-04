class MeetingsController < ApplicationController
  before_filter :authenticate_user!, :except => [:index, :show]
  before_filter :find_meeting, :only => [:show, :update, :edit, :destroy]
  before_filter :ensure_that_user_can_edit, only: [:update, :edit, :destroy]
  def index
    respond_to do |format|
      format.html do
        filter = Meeting.filter(params)
        @meetings = filter.meetings
        @location_filters = filter.location_filters
        @active_location_filters = [*params[:location]]
      end
      format.rss do
        @meetings = Meeting.filter(params).meetings
      end
    end
  end

  def show
  end

  def new
    @meeting = Meeting.new
    @meeting.starts_at = 7.days.from_now.to_date + 16.hours
  end

  def edit
    redirect_to_login unless @meeting.can_be_edited_by current_user
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
    @meeting.user ||= current_user

    if @meeting.update_attributes(params[:meeting])
      redirect_to root_path, notice: 'Meeting was successfully updated.'
    else
      render "edit"
    end
  end

  def destroy
    @meeting.destroy

    redirect_to root_path
  end

private

  def can_approve_meeting?
    user_signed_in?
  end
  
  def find_meeting
    @meeting = Meeting.find(params[:id])
  end

  def ensure_that_user_can_edit
    unless @meeting.can_be_edited_by current_user
      redirect_to new_user_session_path
    end
  end

end

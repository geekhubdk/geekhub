class MeetingsController < ApplicationController
  before_filter :authenticate_user!, :except => [:index, :show]
  before_filter :find_meeting, :only => [:show, :update, :edit, :destroy]

  def index
    respond_to do |format|
      format.html do
        @meetings, @location_filters = Meeting.filter(params, true)
        @active_location_filters = [*params[:location]]
      end
      format.rss do
        @meetings = Meeting.filter(params)
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

    unless @meeting.can_be_edited_by current_user
      redirect_to new_user_session_path
    end

    @meeting.user ||= current_user

    if @meeting.update_attributes(params[:meeting])
      redirect_to root_path, notice: 'Meeting was successfully updated.'
    else
      render "edit"
    end
  end

  def destroy
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
  
  def find_meeting
    @meeting = Meeting.find(params[:id])
  end

end

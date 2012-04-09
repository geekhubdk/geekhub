class MeetingsController < ApplicationController
  before_filter :authenticate_user!, :only => [:destroy, :edit, :update]
  helper_method :can_approve_meeting?

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
      redirect_to root_path, notice: 'Meeting was successfully created.'
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
      redirect_to root_path, notice: 'Meeting was successfully updated.'
    else
      render "edit"
    end
  end

  def destroy
    @meeting = Meeting.find(params[:id])
    @meeting.destroy

    redirect_to root_path
  end

private

  def can_approve_meeting?
    user_signed_in?
  end

end

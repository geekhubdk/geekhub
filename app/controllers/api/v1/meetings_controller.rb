class Api::V1::MeetingsController < ApplicationController
  respond_to :json

  before_filter :authenticate, only: [:create, :new]

  def index
    @meetings = Meeting.upcomming.order("starts_at")
    respond_with meetings_json(@meetings)
  end

  def create

    @meeting = Meeting.new(params[:meeting])
    @meeting.user = @current_user

    if @meeting.save
      render json: @meeting, status: :created, location: @meeting
    else
      render json: @meeting.errors, status: :unprocessable_entity
    end

  end

  private

  def authenticate
    if user = authenticate_with_http_basic { |u, p| User.authenticate(u, p) }
      @current_user = user
    else
      request_http_basic_authentication
    end
  end

  def meetings_json(meetings)
    meetings.collect do |meeting|
      meeting_json(meeting)
    end
  end

  def meeting_json(meeting)
    {
      title: meeting.title,
      starts_at: meeting.starts_at,
      description: meeting.description,
      location: meeting.location,
      url: meeting.url,
      organizer: meeting.organizer,
      costs_money: meeting.costs_money
    }
  end
end
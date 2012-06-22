class Api::V1::MeetingsController < ApplicationController
  respond_to :json, :timeline

  before_filter :authenticate, only: [:create, :new]
  after_filter :set_access_control_headers

  def index
    if params[:all] == "1"
      @meetings = Meeting.order("starts_at").all
    else
      @meetings = Meeting.upcomming.order("starts_at")    
    end

    if params[:organizer]
      @meetings = @meetings.select{|m| param_match(m.organizer,params[:organizer])}
    end
    
    if params[:location]
      @meetings = @meetings.select{|m| param_match(m.location,params[:location])}
    end

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

  def set_access_control_headers 
    headers['Access-Control-Allow-Origin'] = '*' 
    headers['Access-Control-Request-Method'] = '*' 
  end
  
  def param_match value, param
    if param.is_a? Array
      param.any?{|p| p.downcase == value.downcase}
    else    
      value.downcase == param.downcase 
    end
  end

  def authenticate
    if user = authenticate_with_httpb_asic { |u, p| User.authenticate(u, p) }
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
      id: meeting.id,
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
class Api::V1::MeetingsController < ApplicationController
  respond_to :json

  before_filter :authenticate_user!, only: [:create]
  after_filter :set_access_control_headers

  def index
    @meetings = Meeting.filter(params).meetings
    @json = meetings_json(@meetings)
    respond_with @json
  end

private

  def set_access_control_headers 
    headers['Access-Control-Allow-Origin'] = '*' 
    headers['Access-Control-Request-Method'] = '*' 
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
      location: meeting.city.name,
      url: meeting.join_url,
      organizer: meeting.organizer,
      costs_money: meeting.costs_money,
      latitude: meeting.latitude,
      longtitude: meeting.longitude,
      address: meeting.address,
      joinable: meeting.joinable
    }
  end
end

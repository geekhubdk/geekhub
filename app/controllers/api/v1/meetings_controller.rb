class Api::V1::MeetingsController < ApplicationController
  respond_to :json
  def index
    @meetings = Meeting.upcomming.approved.order("starts_at")
    respond_with meetings_json(@meetings)
  end

  private

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
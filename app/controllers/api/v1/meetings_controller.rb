class Api::V1::MeetingsController < ApplicationController
  respond_to :json
  def index
    @meetings = Meeting.upcomming.approved.order("starts_at")
    respond_with meetings_json(@meetings)
  end

  private

  def meetings_json(meetings)
    {
      Meetings: meetings.collect do |meeting|
        meeting_json(meeting)
      end
    }
  end

  def meeting_json(meeting)
    {
      ID: meeting.id,
      Title: meeting.title,
      DateStart: meeting.starts_at,
      Summary: meeting.description,
      Location: meeting.location,
      Link: meeting.url,
      Organizer: meeting.organizer,
      Organiser: meeting.organizer # DEPRECATED: for windows phone support (spelled wrong)
    }
  end
end
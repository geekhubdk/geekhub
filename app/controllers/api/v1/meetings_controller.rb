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
    m = {
      id: meeting.id,
      title: meeting.title,
      starts_at: meeting.starts_at,
      description: meeting.description,
      location: meeting.city.name,
      region: meeting.city.region.try(:name),
      url: meeting.join_url,
      organizer: meeting.organizer,
      costs_money: meeting.costs_money,
      latitude: meeting.latitude,
      longtitude: meeting.longitude,
      address: meeting.address,
      joinable: meeting.joinable,
      tags: meeting.tags.map{|t| tag_json(t)},
      created_at: meeting.created_at,
      attendees: meeting.attendees.map{|a| attendee_json(a)},
      comments: meeting.comments.map{|c| comment_json(c)},
      capacity: meeting.capacity
    }

    if(params[:show_hidden] == "1")
      m["suggested_by"] = meeting.suggested_by
      m["user"] = meeting.user.try(:email)
    end

    return m
  end

  def attendee_json(a)
    if(params[:show_hidden] == "1")
      {name: a.name, email: a.email, twitter: a.twitter}
    else
      {name: a.name, twitter: a.twitter}
    end
  end

  def comment_json(c)
    if(params[:show_hidden] == "1")
      {email: c.email, content: c.content, name: c.name, created_at: c.created_at}
    else
      {content: c.content, name: c.name, created_at: c.created_at}
    end
  end

  def tag_json(t)
    {name: t.name}
  end
end

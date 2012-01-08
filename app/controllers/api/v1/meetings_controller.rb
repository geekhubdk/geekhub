class Api::V1::MeetingsController < ApplicationController
  respond_to :json
  def index
    @meetings = Meeting.upcomming.approved.order("starts_at")
    respond_with @meetings
  end
end
# encoding: UTF-8

class MeetingsController < ApplicationController
  before_filter :authenticate_user!, :except => [:index, :show, :save_filter]
  before_filter :find_meeting, :only => [:show, :update, :edit, :destroy]
  before_filter :ensure_that_user_can_edit, only: [:update, :edit, :destroy]
  
  respond_to :html, :rss, only: [ :index ]
  respond_to :json, only: [ :typeahead_organizers, :typeahead_address ]

  def index
    load_saved_location_filters
    
    filter = Meeting.filter(params)
    @meetings = filter.meetings
    @location_filters = filter.location_filters
    @active_location_filters = [*params[:location]]

    respond_with @meetings
  end

  def show
    @attendees = @meeting.attendees.all
    @new_attendee = @meeting.attendees.new
  end

  def new
    @meeting = Meeting.new
    @meeting.starts_at = 7.days.from_now.to_date + 16.hours
  end

  def edit
  end

  def create
    @meeting = Meeting.new(params[:meeting])

    @meeting.user = current_user

    if @meeting.save
      redirect_to root_path, notice: 'Event tilføjet!'
    else
      render action: "new"
    end
  end

  def update
    @meeting.user ||= current_user

    if @meeting.update_attributes(params[:meeting])
      redirect_to root_path, notice: 'Event opdateret!'
    else
      render "edit"
    end
  end

  def destroy
    @meeting.destroy
    redirect_to root_path
  end


  def save_filter
    cookies.permanent[:location] = params[:location]
    render nothing: true
  end

  def typeahead_address
    query = "%" + params[:query] + "%"
    city = params[:city]
    after = Time.now - 6.months

    if city.blank?
      respond_with Meeting.unscoped
                    .select("DISTINCT(address)")
                    .where("address ILIKE ? AND starts_at >= ? AND address IS NOT NULL", query, after)
                    .collect{|m| m.address}
    else
      respond_with Meeting.unscoped
                    .select("DISTINCT(address)")
                    .where("address ILIKE ? AND starts_at >= ? AND address IS NOT NULL and city_id = ?", query, after, city)
                    .collect{|m| m.address}
    end

  end

  def typeahead_organizers
    query = "%" + params[:query] + "%"
    after = Time.now - 6.months
    respond_with Meeting.unscoped
                   .select("DISTINCT(organizer)")
                   .where("organizer ILIKE ? AND starts_at >= ?", query, after)
                   .collect{|m| m.organizer}
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

  def load_saved_location_filters
    if should_read_location_from_config
      params[:location] = cookies[:location].split("&")
    end
  end

  def should_read_location_from_config
    params[:location].nil? && cookies[:location] != nil
  end

end

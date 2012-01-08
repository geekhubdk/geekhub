class MeetingsController < ApplicationController

  before_filter :authenticate_user!, :only => [:edit, :update, :destroy]

  def index
    @meetings = Meeting.upcomming.order("starts_at")

    if params[:approved] == "0" and can_approve_meeting?
      # approve mode
      @mode = :approve
      @meetings = @meetings.needs_approval  
    else
      @mode = :upcomming
      @meetings = @meetings.approved
    end

    respond_to do |format|
      format.html # index.html.erb
      format.json { render json: @meetings }
      format.rss
    end
  end

  def new
    @meeting = Meeting.new
    @mode = can_approve_meeting? ? :create : :approve

    respond_to do |format|
      format.html # new.html.erb
      format.json { render json: @meeting }
    end
  end

  def edit
    @meeting = Meeting.find(params[:id])
  end

  def create
    @meeting = Meeting.new(params[:meeting])

    @meeting.approved_at = can_approve_meeting? ? Time.now : nil

    respond_to do |format|
      if @meeting.save
        format.html { redirect_to @meeting, notice: 'Meeting was successfully created.' }
        format.json { render json: @meeting, status: :created, location: @meeting }
      else
        format.html { render action: "new" }
        format.json { render json: @meeting.errors, status: :unprocessable_entity }
      end
    end
  end

  def update
    @meeting = Meeting.find(params[:id])

    unless params[:approve].nil?
      @meeting.approved_at = Time.now
    end

    respond_to do |format|
      if @meeting.update_attributes(params[:meeting])
        format.html { redirect_to meetings_path, notice: 'Meeting was successfully updated.' }
        format.json { head :ok }
      else
        format.html { render action: "edit" }
        format.json { render json: @meeting.errors, status: :unprocessable_entity }
      end
    end
  end

  def destroy
    @meeting = Meeting.find(params[:id])
    @meeting.destroy

    respond_to do |format|
      format.html { redirect_to meetings_url }
      format.json { head :ok }
    end
  end

  private

  def can_approve_meeting?
    user_signed_in?
  end
end

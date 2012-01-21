class MeetingRevisionsController < ApplicationController
  
  before_filter :authenticate_user!, :only => [:edit, :approve]

  def edit
    @revision = MeetingRevision.find(params[:id])
    @meeting = @revision.meeting
  end

  def approve
    @revision = MeetingRevision.find(params[:id])
    @revision.approve
    redirect_to meetings_path, notice: 'Aendringen blev godkendt'
  end
end
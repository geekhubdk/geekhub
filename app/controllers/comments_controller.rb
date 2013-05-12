class CommentsController < ApplicationController
  def create
    if params["no-spammy-spammy"] == "1"

      @meeting = Meeting.find(params[:meeting_id])
      @comment = @meeting.comments.new(params[:comment])

      if @comment.save
        redirect_to @meeting, notice: t("comment.added")
      else
        flash[:error] = t("comment.validation_failed")
        redirect_to @meeting
      end
    else  
      flash[:error] = t("comment.validation_failed")
      redirect_to @meeting
    end
  end
end

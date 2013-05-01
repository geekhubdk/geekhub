class CommentsController < ApplicationController
  def create
    @meeting = Meeting.find(params[:meeting_id])
    @comment = @meeting.comments.new(params[:comment])

    if @comment.save
      redirect_to @meeting, notice: t("comment.added")
    else
      flash[:error] = t("comment.validation_failed")
      redirect_to @meeting
    end
  end
end

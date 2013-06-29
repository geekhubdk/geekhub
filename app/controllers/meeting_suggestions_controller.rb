# encoding: UTF-8

class MeetingSuggestionsController < ApplicationController
  
  def index
    respond_to do |format|
      format.rss do
        @meeting_suggestions = MeetingSuggestion.all
      end
    end
  end

  def create
    @meeting_suggestion = MeetingSuggestion.new(params[:meeting_suggestion])

    if @meeting_suggestion.save
      redirect_to root_path, notice: t('meeting_suggestion.created')
    else
      redirect_to root_path
    end
  end
end

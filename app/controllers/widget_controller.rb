class WidgetController < ApplicationController
  def show
    headers['X-Frame-Options'] = "ALLOWALL"
    filter = Meeting.filter(params)
    @meetings = filter.meetings.take(5)
    @title = params[:title] || "Udvikler events i danmark"
    render :show, :layout => false 
  end

  def example
  end
end

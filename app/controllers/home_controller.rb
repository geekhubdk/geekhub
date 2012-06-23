class HomeController < ApplicationController
  def index
    @meetings, @location_filters = Meeting.filter(params, true)
    if params[:location].is_a? String
    	@active_location_filters = [params[:location]]
    else
    	@active_location_filters = params[:location]
    end

    @active_location_filters ||= []
  end
end

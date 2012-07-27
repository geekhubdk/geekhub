class HomeController < ApplicationController
  def index
    @meetings, @location_filters = Meeting.filter(params, true)
   	@active_location_filters = [*params[:location]]
  end
end

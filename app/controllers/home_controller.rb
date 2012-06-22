class HomeController < ApplicationController
  def index
    @all_upcomming = Meeting.filter(params)
  end
end

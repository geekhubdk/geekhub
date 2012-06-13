class HomeController < ApplicationController
  def index
    @newest_approved = Meeting.order("created_at desc").limit(3)
    @all_upcomming = Meeting.upcomming.order("starts_at")
  end
end

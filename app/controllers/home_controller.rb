class HomeController < ApplicationController
  def index
    @newest_approved = Meeting.approved.order("approved_at desc").limit(3)
    @all_upcomming = Meeting.approved.upcomming.order("starts_at")
  end
end

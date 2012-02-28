class HomeController < ApplicationController
  def index
    @next_seven_days = Meeting.upcomming.approved.where("starts_at < ?", Date.today + 8.days).order("starts_at")

    # we will show the same amount of newest meetings as the number of current in this week. But at least five
    number_of_newest_to_show = @next_seven_days.length
    number_of_newest_to_show = 5 if number_of_newest_to_show < 5
    
    @newest_approved = Meeting.approved.order("approved_at desc").limit(number_of_newest_to_show)
    @all_upcomming = Meeting.approved.upcomming.order("starts_at")
  end
end

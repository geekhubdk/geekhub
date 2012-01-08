class Meeting < ActiveRecord::Base
  scope :upcomming, where("starts_at >= ?", Date.today + 1.day)
  scope :approved, where("approved_at is not null")
  scope :needs_approval, where("approved_at is null")

  def needs_approval?
    approved_at.blank?
  end

end

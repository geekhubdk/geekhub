class Meeting < ActiveRecord::Base
  validates :title, :starts_at, :location, :organizer, :description, :url, :presence => true
  scope :upcomming, lambda { where("starts_at >= ?", Date.today) }
  scope :approved, where("approved_at is not null")
  scope :needs_approval, where("approved_at is null")

  def needs_approval?
    approved_at.blank?
  end

  def month
    self.starts_at.strftime('%m')
  end
end

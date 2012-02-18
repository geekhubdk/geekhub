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

  def to_ical
    RiCal.Calendar do |cal|
      cal.event do |event|
        event.summary = self.title
        event.dtstart =  self.starts_at
        event.dtend = self.starts_at + 1.hour
        event.location = self.location
      end
    end
  end
end

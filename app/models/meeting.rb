class Meeting < ActiveRecord::Base
  scope :upcomming, where("starts_at >= ?", Date.today + 1.day)
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
        event.description = self.title
        event.dtstart =  self.starts_at
        event.dtend = self.starts_at + 1.hour
        event.location = self.location
      end
    end
  end
end

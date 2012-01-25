class MeetingRevision < ActiveRecord::Base
  belongs_to :meeting

  validates :title, :starts_at, :location, :organizer, :description, :url, :presence => true

  scope :upcomming, lambda { where("starts_at >= ?", Date.today + 1.day) }
  scope :needs_approval, where("approved_at is null")

  def month
    self.starts_at.strftime('%m')
  end

  def approve
    meeting = self.meeting
    self.approved_at = Time.now
    meeting.title = self.title
    meeting.costs_money = self.costs_money
    meeting.starts_at = self.starts_at
    meeting.location = self.location
    meeting.organizer = self.organizer
    meeting.description = self.description
    meeting.url = self.url
    meeting.save
    self.save
  end

end

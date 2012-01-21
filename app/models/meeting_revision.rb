class MeetingRevision < ActiveRecord::Base
  belongs_to :meeting

  validates :title, :starts_at, :location, :organizer, :description, :url, :presence => true

  scope :upcomming, where("starts_at >= ?", Date.today + 1.day)
 
  def month
    self.starts_at.strftime('%m')
  end

end

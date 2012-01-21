class MeetingRevision < ActiveRecord::Base
  belongs_to :meeting

  validates :title, :starts_at, :location, :organizer, :description, :url, :presence => true
end

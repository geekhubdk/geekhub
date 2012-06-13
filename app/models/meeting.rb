class Meeting < ActiveRecord::Base
  validates :title, :starts_at, :location, :organizer, :description, :url, :presence => true
  scope :upcomming, lambda { where("starts_at >= ?", Date.today) }

  belongs_to :user

  def month
    self.starts_at.strftime('%m')
  end

  def self.filter(filters)
    m = Meeting.order("starts_at")
    m = m.upcomming if filters[:upcomming] == true
    if filters[:days_from_now].to_i > 0
      m = m.where("starts_at < ?", Time.now + filters[:days_from_now].to_i.days)
    end
    return m
  end

  def can_be_edited_by user
    user_id.nil? || user_id == user.id
  end
end

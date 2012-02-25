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

  def self.filter(filters)
    m = Meeting.order("starts_at")
    m = m.upcomming if filters[:upcomming] == true
    m = filters[:approved] ? m.approved : m.needs_approval
    if filters[:days_from_now].to_i > 0
      m = m.where("starts_at < ?", Time.now + filters[:days_from_now].to_i.days)
    end
    return m
  end
end

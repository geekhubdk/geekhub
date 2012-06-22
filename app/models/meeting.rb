  class Meeting < ActiveRecord::Base
  validates :title, :starts_at, :location, :organizer, :description, :url, :presence => true
  scope :upcomming, lambda { includes(:meeting_votes).where("starts_at >= ?", Date.today) }

  belongs_to :user
  has_many :meeting_votes 

  def month
    self.starts_at.strftime('%m')
  end

  def self.filter(filters)
    m = Meeting.includes(:meeting_votes).order("starts_at")
    m = m.upcomming if filters[:all] != "1"
    
    if filters[:organizer]
      m = m.select{|m| param_match(m.organizer,filters[:organizer])}
    end
    
    if filters[:location]
      m = m.select{|m| param_match(m.location,filters[:location])}
    end

    return m
  end

  def can_be_edited_by user
    user_id.nil? || user_id == user.id
  end

  private

  def self.param_match value, param
    if param.is_a? Array
      param.any?{|p| p.downcase == value.downcase}
    else    
      value.downcase == param.downcase 
    end
  end

end

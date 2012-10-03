  class Meeting < ActiveRecord::Base
  validates :title, :starts_at, :location, :organizer, :description, :url, :presence => true
  scope :upcoming, lambda { where("starts_at >= ?", Date.today) }

  belongs_to :user

  geocoded_by :location  # can also be an IP address
  after_validation :geocode          # auto-fetch coordinates

  def month
    self.starts_at.strftime('%m')
  end

  def self.filter(filters, return_filters = false)
    m = Meeting.order("starts_at")
    m = m.upcoming if filters[:all] != "1"

    location_filters = m.map{|x| x.location}.uniq

    if filters[:organizer] && !filters[:organizer].blank?
      m = m.select{|x| param_match(x.organizer,filters[:organizer])}
    end
    
    if filters[:location] && !filters[:location].blank?
      m = m.select{|x| param_match(x.location,filters[:location])}
    end

    return [m, location_filters] if return_filters
    return m unless return_filters
  end

  def can_be_edited_by user
    user_id.nil? || user_id == user.id || user.email == "deldy@deldysoft.dk"
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

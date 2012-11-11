  class Meeting < ActiveRecord::Base
  validates :title, :starts_at, :city_id, :organizer, :description, :url, :presence => true
  scope :upcoming, lambda { where("starts_at >= ?", Date.today) }

  belongs_to :user
  belongs_to :city

  geocoded_by :geocode_location  # can also be an IP address
  after_validation :geocode          # auto-fetch coordinates

  def month
    self.starts_at.strftime('%m')
  end

  def geocode_location
    unless address.blank?
      address
    else
      city.name unless city.nil?
    end
  end

  def self.filter(filters)
    m = Meeting.includes(:city => :region).order("starts_at")
    m = m.upcoming if filters[:all] != "1"

    location_filters = m.select{|x| x.city != nil}.map{|x| x.city}.uniq

    m = m.select{|x| param_match(x.organizer,filters[:organizer])}
    m = m.select{|x| param_match(x.city.try(:name),filters[:location])}

    result = Struct.new(:meetings,:location_filters)
    result.new(m, location_filters)
  end

  def can_be_edited_by user
    user_id.nil? || user_id == user.id || user.email == "deldy@deldysoft.dk"
  end

private

  def self.param_match value, param
    param.blank? || [*param].any?{|p| value.nil? == false && p.downcase == value.downcase}  
  end

end

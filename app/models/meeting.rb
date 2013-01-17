  class Meeting < ActiveRecord::Base
  validates :title, :starts_at, :city_id, :organizer, :description, :url, :presence => true
  scope :upcoming, lambda { where("starts_at >= ?", Date.today) }

  default_scope order("starts_at").includes(:city => :region)

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
    m = filters[:all] == "1" ? Meeting.all : Meeting.upcoming

    location_filters = build_location_filters(m)

    m = m.select{|x| param_match(x.organizer,filters[:organizer])}
    m = m.select{|x| param_match(x.city.name,filters[:location])}

    MeetingFilterResult.new(m, location_filters)
  end

  def can_be_edited_by user
    user_id.nil? || user_id == user.id || user.email == "deldy@deldysoft.dk"
  end

  class MeetingFilterResult
    attr_reader :meetings, :location_filters

    def initialize meetings, location_filters
      @meetings = meetings
      @location_filters = location_filters
    end
  end
private

  def self.param_match value, param
    return true if value.nil?
    param.blank? || [*param].any?{|p| p.downcase == value.downcase}  
  end

  def self.build_location_filters meetings
    meetings.map{|x| x.city}.uniq
  end
end

  class Meeting < ActiveRecord::Base
  validates :title, :starts_at, :city_id, :organizer, :description, :presence => true
  validates :url, presence: true, :unless => :joinable
  validates :capacity, :numericality => { :greater_than => 0 }, :allow_nil => true

  scope :upcoming, lambda { where("starts_at >= ?", Date.today) }

  default_scope order("starts_at").includes(:city => :region)

  belongs_to :user
  belongs_to :city

  has_many :attendees

  geocoded_by :geocode_location
  after_validation :geocode

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

  def join_url
    return url unless joinable

    "http://geekhub.dk/meetings/#{self.id}"
  end

  def can_add_attendee email
    self.attendees.all.select{|a| a.email.downcase == email.downcase}.count == 0
  end

  def can_attend?
    self.joinable && self.starts_at.future? && !reached_capacity?
  end

  def can_detend?
    self.joinable && self.starts_at.future?
  end

  def reached_capacity?
    return false if capacity.nil?

    attendees.count >= capacity
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

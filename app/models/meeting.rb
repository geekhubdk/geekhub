class Meeting < ActiveRecord::Base
  validates :title, :starts_at, :city_id, :organizer, :description, :presence => true
  validates :url, presence: true, :unless => :joinable
  validates :capacity, :numericality => { :greater_than => 0 }, :allow_nil => true

  scope :upcoming, ->{ where('starts_at >= ?', Date.today) }

  default_scope { order('starts_at').includes(:city => :region) }

  belongs_to :user
  belongs_to :city
  
  has_many :attendees
  has_many :meeting_email_alerts
  has_many :meeting_tweet_alerts
  has_many :meeting_tags, :dependent => :destroy
  has_many :tags, :through => :meeting_tags
  has_many :comments, :as => :commentable

  attr_writer :tag_names

  geocoded_by :geocode_location
  after_validation :geocode

  after_save :assign_tags
  
  def tag_names
    @tag_names || tags.map(&:name).join(',')
  end

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
    m = filters[:all] == '1' ? Meeting.includes(:meeting_tags => :tag) : Meeting.upcoming.includes(:meeting_tags => :tag)

    location_filters = build_location_filters(m)
    m = m.select{|x| param_match(x.tags.map(&:name),filters[:tag])}
    m = m.select{|x| param_match(x.organizer,filters[:organizer])}
    m = m.select{|x| param_match(x.city.name,filters[:location])}
    m = m.select{|x| param_match(x.city.region.try(:name),filters[:region])}

    MeetingFilterResult.new(m, location_filters)
  end

  def can_be_edited_by user
    if user.nil?
      false
    else
      user_id.nil? || user_id == user.id || user.email == 'deldy@deldysoft.dk'
    end
  end

  def join_url
    return url unless joinable

    "http://www.geekhub.dk/meetings/#{self.id}"
  end

  def can_add_attendee email
    self.attendees.select{|a| a.email.downcase == email.downcase}.count == 0
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
  
  def commentable?
    true
  end

  def is_online
    self.city.try(:name) == 'Online'
  end

  def self.available_for_alerts
    Meeting.upcoming.includes(:meeting_email_alerts).where('meeting_email_alerts.id IS NULL and meetings.created_at < ?', 3.hours.ago).references(:meeting_email_alerts)
  end
  
  def self.available_for_tweet_alerts
    Meeting.upcoming.includes(:meeting_tweet_alerts).where('meeting_tweet_alerts.id IS NULL and meetings.created_at < ?', 5.minutes.ago).references(:meeting_email_alerts)
  end

private

  def assign_tags
    if @tag_names
      self.tags = @tag_names.split(/,+/).map do |name|
        Tag.find_or_create_by(name: name)
      end
    end
  end

  def self.param_match value, param
    return true unless param.present?
    return false if [*value].empty?
    param.blank? || [*param].any?{|p| [*value].any?{|v| p.downcase == v.downcase}}
  end

  def self.build_location_filters meetings
    meetings.map{|x| x.city}.uniq
  end

end

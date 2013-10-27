class MeetingFilterResult
  attr_reader :meetings, :location_filters

  def initialize meetings, location_filters
    @meetings = meetings
    @location_filters = location_filters
  end
end
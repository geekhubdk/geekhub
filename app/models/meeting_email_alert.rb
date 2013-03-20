class MeetingEmailAlert < ActiveRecord::Base
  attr_accessible :emails, :meeting_id
  belongs_to :meeting
end

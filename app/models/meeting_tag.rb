class MeetingTag < ActiveRecord::Base
  belongs_to :meeting
  belongs_to :tag
end

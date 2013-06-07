class MeetingTweetAlert < ActiveRecord::Base
  belongs_to :meeting
  # attr_accessible :title, :body
end

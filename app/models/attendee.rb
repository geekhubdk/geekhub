class Attendee < ActiveRecord::Base
  attr_accessible :email, :meeting_id, :name, :user_id

  belongs_to :meeting
  belongs_to :user
end

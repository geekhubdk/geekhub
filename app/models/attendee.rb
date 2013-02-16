class Attendee < ActiveRecord::Base
  attr_accessible :email, :meeting_id, :name, :user_id, :twitter

  validates :meeting, :email, :name, presence: true
  validates :email, :uniqueness => {:scope => :meeting_id, :case_sensitive => false}

  belongs_to :meeting, :counter_cache => true
  belongs_to :user
end

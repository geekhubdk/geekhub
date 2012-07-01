class Organizer < ActiveRecord::Base
  attr_accessible :description, :name, :url
  validates :name, :url, presence: true
  
  def can_be_edited_by user
    true # everyone can edit organizers for now
  end
end

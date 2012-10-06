class City < ActiveRecord::Base
  attr_accessible :name
  has_many :meetings
  belongs_to :region
end

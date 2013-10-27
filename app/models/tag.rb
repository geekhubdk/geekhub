class Tag < ActiveRecord::Base
  has_many :meeting_tags, :dependent => :destroy
  has_many :meetings, :through => :meeting_tags

  before_save :downcase_and_trim_name

  def downcase_and_trim_name
    self.name = self.name.downcase.strip
  end
end

class Member < ActiveRecord::Base
  has_secure_password
  
  validates :email, :name, :presence => true
  validates :password, :presence => true, :on => :create
  
  belongs_to :location
  
  acts_as_mappable :default_units => :kms

  def self.find_by_email_and_password(email, password)
    find_by_email(email).try(:authenticate, password)
  end
end

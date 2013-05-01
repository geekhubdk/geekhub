class Comment < ActiveRecord::Base
  attr_accessible :commentable_id, :commentable_type, :content, :email, :name
  belongs_to :commentable, :polymorphic => true
  
  validates :email, :name, :content, presence: true
end

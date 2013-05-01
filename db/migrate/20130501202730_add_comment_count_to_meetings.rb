class AddCommentCountToMeetings < ActiveRecord::Migration
  def self.up
    add_column :meetings, :comments_count, :integer, :default => 0

    Meeting.reset_column_information
    Meeting.find(:all).each do |p|
      Meeting.update_counters p.id, :comments_count => p.comments.length
    end
  end

  def self.down
    remove_column :meetings, :comments_count
  end
end

class AddAttendeesCountToMeeting < ActiveRecord::Migration
  def change
    add_column :meetings, :attendees_count, :integer, :default => 0

    Meeting.reset_column_information
    Meeting.find(:all).each do |m|
      Meeting.update_counters m.id, :attendees_count => m.attendees.length
    end
  end
end

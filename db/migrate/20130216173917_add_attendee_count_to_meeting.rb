class AddAttendeeCountToMeeting < ActiveRecord::Migration
  def change
    add_column :meetings, :attendee_count, :integer, :default => 0

    Meeting.reset_column_information
    Meeting.find(:all).each do |m|
      Meeting.update_counters m.id, :attendee_count => m.attendees.length
    end
  end
end

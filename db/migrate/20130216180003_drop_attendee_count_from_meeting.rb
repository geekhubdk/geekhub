class DropAttendeeCountFromMeeting < ActiveRecord::Migration
  def up
    remove_column :meetings, :attendee_count
  end

  def down
  end
end

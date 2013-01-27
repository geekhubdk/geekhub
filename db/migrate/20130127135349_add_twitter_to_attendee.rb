class AddTwitterToAttendee < ActiveRecord::Migration
  def change
    add_column :attendees, :twitter, :string
  end
end

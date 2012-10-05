class RemoveLocationFromMeeting < ActiveRecord::Migration
  def up
    remove_column :meetings, :location
  end

  def down
  end
end

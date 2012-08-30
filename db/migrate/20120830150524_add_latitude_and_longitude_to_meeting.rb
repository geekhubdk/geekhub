class AddLatitudeAndLongitudeToMeeting < ActiveRecord::Migration
  def change
    add_column :meetings, :latitude, :float
    add_column :meetings, :longitude, :float
  end
end
